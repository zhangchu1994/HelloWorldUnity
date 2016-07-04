using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Chronos
{
	public interface IRecorder
	{
		void Reset();
		int EstimateMemoryUsage();
	}

	public abstract class RecorderTimeline<TComponent, TSnapshot> : ComponentTimeline<TComponent>, IRecorder where TComponent : Component
	{
		public RecorderTimeline(Timeline timeline) : base(timeline)
		{
			enableRecording = true;
			snapshots = new List<TSnapshot>();
			times = new List<float>();
		}

		public override void Start()
		{
			Reset();
		}

		public override void Update()
		{
			float timeScale = timeline.timeScale;

			if (lastTimeScale >= 0 && timeScale < 0) // Started rewind
			{
				laterSnapshot = CopySnapshot();
				laterTime = timeline.time;
				interpolatedSnapshot = laterSnapshot;
				canRewind = TryFindEarlierSnapshot(false);
			}

			if (timeScale > 0)
			{
				Progress();
			}
			else if (timeScale < 0)
			{
				Rewind();
			}

			lastTimeScale = timeScale;
		}

		#region Fields

		protected List<TSnapshot> snapshots;
		protected List<float> times;
		protected int capacity;
		protected float recordingTimer;
		protected float lastTimeScale = 1;
		protected bool canRewind;
		protected TSnapshot laterSnapshot;
		protected float laterTime;
		protected TSnapshot earlierSnapshot;
		protected float earlierTime;
		protected TSnapshot interpolatedSnapshot;

		#endregion

		#region Properties

		/// <summary>
		/// Indicates whether the recorder has exhausted its rewind capacity. 
		/// </summary>
		public bool exhaustedRewind
		{
			get { return !canRewind; }
		}

		/// <summary>
		/// Whether the timeline should save snapshots for rewinding.
		/// </summary>
		public bool enableRecording { get; set; }

		#endregion

		#region Flow

		protected void Progress()
		{
			if (recordingTimer >= timeline.recordingInterval)
			{
				Record();

				recordingTimer = 0;
			}

			recordingTimer += timeline.deltaTime;
		}

		protected void Record()
		{
			if (!enableRecording)
			{
				return;
			}

			if (snapshots.Count == capacity)
			{
				snapshots.RemoveAt(0);
				times.RemoveAt(0);
			}

			snapshots.Add(CopySnapshot());
			times.Add(timeline.time);

			canRewind = true;
		}

		protected void Rewind()
		{
			if (canRewind)
			{
				if (timeline.time <= earlierTime)
				{
					canRewind = TryFindEarlierSnapshot(true);

					if (!canRewind)
					{
						// Make sure the last snapshot is perfectly in place
						interpolatedSnapshot = earlierSnapshot;
						ApplySnapshot(interpolatedSnapshot);

						timeline.SendMessage("OnExhaustRewind", SendMessageOptions.DontRequireReceiver);

						return;
					}
				}

				float t = (laterTime - timeline.time) / (laterTime - earlierTime);

				interpolatedSnapshot = LerpSnapshots(laterSnapshot, earlierSnapshot, t);

				ApplySnapshot(interpolatedSnapshot);
			}
		}

		protected void OnExhaustRewind()
		{
			if (Timekeeper.instance.debug)
			{
				Debug.LogWarning("Reached rewind limit.");
			}
		}

		#endregion

		#region Snapshots

		protected abstract TSnapshot LerpSnapshots(TSnapshot from, TSnapshot to, float t);

		protected abstract TSnapshot CopySnapshot();

		protected abstract void ApplySnapshot(TSnapshot snapshot);

		protected bool TryFindEarlierSnapshot(bool pop)
		{
			if (pop)
			{
				if (snapshots.Count < 1)
				{
					return false;
				}

				laterSnapshot = snapshots[snapshots.Count - 1];
				laterTime = times[times.Count - 1];

				snapshots.RemoveAt(snapshots.Count - 1);
				times.RemoveAt(times.Count - 1);
			}

			if (snapshots.Count < 1)
			{
				return false;
			}

			earlierSnapshot = snapshots[snapshots.Count - 1];
			earlierTime = times[times.Count - 1];

			return true;
		}

		public virtual void Reset()
		{
			if (timeline.recordingDuration < timeline.recordingInterval)
			{
				throw new ChronosException("The recording duration must be longer than or equal to interval.");
			}

			if (timeline.recordingInterval <= 0)
			{
				throw new ChronosException("The recording interval must be positive.");
			}

			if (Application.isPlaying)
			{
				snapshots.Clear();
				times.Clear();

				capacity = Mathf.CeilToInt(timeline.recordingDuration / timeline.recordingInterval);
				snapshots.Capacity = capacity;
				times.Capacity = capacity;
				recordingTimer = 0;

				Record();
			}
		}

		#endregion

		internal static int EstimateMemoryUsage(float duration, float interval)
		{
			int structSize = Marshal.SizeOf(typeof(TSnapshot));
			int structAmount = Mathf.CeilToInt(duration / interval);
			int pointerAmount = 1;
			while (pointerAmount < structAmount) pointerAmount *= 2;
			int pointerSize = IntPtr.Size;

			return (structSize * structAmount) + (pointerSize * pointerAmount);
		}

		public int EstimateMemoryUsage()
		{
			return EstimateMemoryUsage(timeline.recordingDuration, timeline.recordingInterval);
		}
	}
}
