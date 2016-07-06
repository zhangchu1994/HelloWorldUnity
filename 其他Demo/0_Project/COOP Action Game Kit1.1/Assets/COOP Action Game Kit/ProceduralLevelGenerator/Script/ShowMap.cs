using UnityEngine;
using System.Collections;

public class ShowMap : MonoBehaviour {

	public Font MyFont;
	public ProceduralLevelGenerator DG;
	public string mapWidth;
	public string mapHeight;
	public string seed;
	public string WallPercentage;
    public string EnemySpawnerDistance;
    public string DecorationSpawnerDistance;
    public string BreakableObjSpawnerDistance;
    public string TreasureObjSpawnerDistance;
    
    public bool GameplayElements;
	public bool SeedUse = false;
	public bool Controlling = true;
	// Use this for initialization
	void Start () {
		mapWidth=DG.MAP_WIDTH.ToString();
		mapHeight=DG.MAP_HEIGHT.ToString();
		SeedUse=DG.useSeed;
		seed=DG.Seed.ToString();
		WallPercentage=DG.WALL_PERCENTAGE.ToString();
		TreasureObjSpawnerDistance = DG.TreasureObjSpawnerDistance.ToString();
		EnemySpawnerDistance = DG.EnemySpawnerDistance.ToString();
		BreakableObjSpawnerDistance = DG.BreakableObjSpawnerDistance.ToString();
		DecorationSpawnerDistance = DG.DecorationSpawnerDistance.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		var translation = Vector3.zero;
		if (Input.GetButtonDown("Jump"))
		{
			DG.MAP_WIDTH=int.Parse(mapWidth);
			DG.MAP_HEIGHT=int.Parse(mapHeight);
			int test=0;
			if(int.TryParse(seed,out test)) {
				DG.Seed=int.Parse(seed);
			} else  {
				DG.Seed=seed.GetHashCode();
			}
			DG.useSeed=SeedUse;
			DG.WALL_PERCENTAGE=int.Parse(WallPercentage);
			DG.TreasureObjSpawnerDistance=int.Parse(TreasureObjSpawnerDistance);
			DG.EnemySpawnerDistance=int.Parse(EnemySpawnerDistance);
			DG.BreakableObjSpawnerDistance=int.Parse(BreakableObjSpawnerDistance);
			DG.DecorationSpawnerDistance=int.Parse(DecorationSpawnerDistance);
			DG.DoGenerate();
		}
		
		var zoomDelta = Input.GetAxis("Mouse ScrollWheel")*30*Time.deltaTime;
        if (zoomDelta!=0)
        {
            translation -= Vector3.up * 30 * zoomDelta;
        }
		
		if(Controlling) {
	    	translation += new Vector3(Input.GetAxis("HorizontalA"), 0, Input.GetAxis("VerticalA"));
	    	GetComponent<Camera>().transform.position += translation;
	    }
	}
	
	void OnGUI() {
	    GUI.skin.font = MyFont;
	    mapWidth = GUI.TextField(new Rect(100, 10, 50, 20),mapWidth, 25);
	    mapHeight = GUI.TextField(new Rect(270, 10, 50, 20),mapHeight, 25);
	    seed = GUI.TextField(new Rect(110, 90, 100, 20),seed, 25);
	    SeedUse = GUI.Toggle(new Rect(20, 90, 80, 50), SeedUse, "Seed Use");
	    WallPercentage = GUI.TextField(new Rect(505, 10, 50, 20),WallPercentage, 25);
	    if(GameplayElements) {
		    TreasureObjSpawnerDistance = GUI.TextField(new Rect(100, 30, 50, 20),TreasureObjSpawnerDistance, 25);
		    EnemySpawnerDistance = GUI.TextField(new Rect(270, 30, 30, 20),EnemySpawnerDistance, 25);
		    BreakableObjSpawnerDistance = GUI.TextField(new Rect(460, 30, 50, 20),BreakableObjSpawnerDistance, 25);
		    DecorationSpawnerDistance = GUI.TextField(new Rect(620, 30, 50, 20),DecorationSpawnerDistance, 25);
		}
		   
	    GUI.Label(new Rect(10, 10, 150, 100), "Map Width");
	    GUI.Label(new Rect(360, 90, 150, 100), "Used Seed "+DG.UsedSeed);
	    GUI.Label(new Rect(180, 10, 150, 100), "Map Height");
	    GUI.Label(new Rect(350, 10, 150, 100), "Wall Percentage (Max 70)");
	    if(GameplayElements) {
		    GUI.Label(new Rect(10, 30, 150, 100), "Treasure Dist");
		    GUI.Label(new Rect(180, 30, 150, 100), "Enemy Dist");
		    GUI.Label(new Rect(350, 30, 150, 100), "Breakable Dist");
		    GUI.Label(new Rect(520, 30, 150, 100), "Decoration Dist");
        	GUI.Label(new Rect(10, 70, 800, 100), "Gameplay Elements are scaterred based on the distances selected (second line).");
	    }
        GUI.Label(new Rect(10, 50, 800, 100), "Space Bar to generate a new level. Move camera with WASD. Mouse Scroll Wheel to zoom.");
        
        if (GUI.Button(new Rect(555, 50, 100, 35), "Play this level!"))
            PlayThisLevel();
            
         if (GUI.Button(new Rect(555, 85, 100, 35), "Back to Menu!"))
            Application.LoadLevel("MainMenu");
    }
    
    void PlayThisLevel() {
    	GameManager.instance.MAP_WIDTH=DG.MAP_WIDTH;
		GameManager.instance.MAP_HEIGHT = DG.MAP_HEIGHT;
		GameManager.instance.WALL_PERCENTAGE = DG.WALL_PERCENTAGE;
		GameManager.instance.TreasureObjSpawnerDistance = DG.TreasureObjSpawnerDistance;
		GameManager.instance.EnemySpawnerDistance = DG.EnemySpawnerDistance;
		GameManager.instance.BreakableObjSpawnerDistance = DG.BreakableObjSpawnerDistance;
		GameManager.instance.DecorationSpawnerDistance = DG.DecorationSpawnerDistance;
		GameManager.instance.seed=DG.UsedSeed;
		GameManager.instance.useSeed=true;
		StartCoroutine(GameManager.instance.PlaySeededLevel());
    }
}