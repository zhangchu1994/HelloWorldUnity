using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ProceduralLevelGenerator : MonoBehaviour {
		public int MAP_WIDTH = 100;

        public int MAP_HEIGHT = 50;
        public int WALL_PERCENTAGE = 45;
        
        [HideInInspector]
        public string CurrentMap;
		
		public GameObject Floor;
		
		
		public bool useSeed=false;
		public int Seed = 0;
		[HideInInspector]
		public int UsedSeed;
		
		public bool SpawnPlayers=false;
 
        public GameObject[] FilledSpaceArray;
        public GameObject[] StraightWallArray;
        public GameObject[] CurvedInsideWallArray;
        public GameObject[] CurvedOutsideWallArray;
        public GameObject[] ColumnWallArray;
        public GameObject[] DecorArray;
        public GameObject[] BreakableObjectsArray;
        public GameObject[] EnemySpawnersArray;
        public GameObject[] TreasureArray;
        public GameObject[] SpecialTreasureArray;
        [HideInInspector]
        public GameObject[] GoalItemsArray;
        public GameObject GoalLevel;
       
        // Prefabs and Instance Management
        GameObject containerWallRooms;
        GameObject containerDecoration;
        GameObject containerEnemies;
        GameObject containerSpecialObjects;
        GameObject containerFilled;
       
        // Player      
        public GameObject SpawnPoint;
       
       
        public int tileSize=3;
        public int EnemySpawnerDistance = 10;
        public int DecorationSpawnerDistance = 6;
        public int BreakableObjSpawnerDistance = 6;
        public int TreasureObjSpawnerDistance = 6;
        [HideInInspector]
        public int ShipPartsSpawnerDistance = 6;
       
		[HideInInspector]
        public int[,] Map;
 
        public int MapWidth             { get; set; }
        public int MapHeight            { get; set; }
        public int PercentAreWalls      { get; set; }
 
		[HideInInspector]
        public bool bUse2Steps;
 
		[HideInInspector]
        public int maxIndex = 0;
        [HideInInspector]
        public List<Vector2> ListDecorOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListDecorCurrent = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListPlayerGoalOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListBreakableObjectsOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListBreakableObjectsCurrent = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListTreasureObjectsOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListTreasureObjectsCurrent = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListSpecialObjects = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListEnemySpawnerOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListEnemySpawnerCurrent = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListShipPartsOptions = new List<Vector2>();
        [HideInInspector]
        public List<Vector2> ListShipPartsCurrent = new List<Vector2>();
       
        List<Vector3> SpawnedObjectsPos = new List<Vector3>();
        
    	bool goalspawned=false;
    	int badresult=0;
       
        public void MapHandler()
        {
                MapWidth = MAP_WIDTH;
                MapHeight = MAP_HEIGHT;
                PercentAreWalls = WALL_PERCENTAGE;
                
                containerWallRooms = new GameObject();
                containerWallRooms.name = "Surrounding Walls";
                containerWallRooms.transform.parent = transform;
                containerDecoration = new GameObject();
                containerDecoration.name = "Decoration";
                containerDecoration.transform.parent = transform;
                containerEnemies = new GameObject();
                containerEnemies.name = "Enemies";
                containerEnemies.transform.parent = transform;
                containerSpecialObjects = new GameObject();
                containerSpecialObjects.name = "Special objects";
                containerSpecialObjects.transform.parent = transform;
                containerFilled = new GameObject();
                containerFilled.name = "FillingObjects";
                containerFilled.transform.parent = transform;
                
                if(Floor) {
					Floor.transform.position=new Vector3 (MAP_WIDTH*3/2,0, MAP_HEIGHT*3/2);
					Floor.transform.localScale=new Vector3 (MAP_WIDTH/3.5f,1, MAP_HEIGHT/3.5f);
					Floor.GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(MAP_WIDTH/3.4f, MAP_HEIGHT/3.4f);
				}
                RandomFillMap();
        }
 
        public void MakeCaverns()
        {
                // By initilizing column in the outter loop, its only created ONCE
                for(int column=0, row=0; row <= MapHeight-1; row++)
                {
                        for(column = 0; column <= MapWidth-1; column++)
                        {
                                Map[column,row] = PlaceWallLogic(column,row);
                        }
                }
        }
 
        public void RemoveClutter()
        {
                // By initilizing column in the outter loop, its only created ONCE
                for(int column=0, row=0; row <= MapHeight-1; row++)
                {
                        for(column = 0; column <= MapWidth-1; column++)
                        {
                                Map[column,row] = RemoveExtraWallsLogic(column,row,Map[column,row]);
                        }
                }
        }
 
        public int RemoveExtraWallsLogic(int x,int y,int regularValue)
        {
                int numWalls = GetAdjacentWalls(x,y,4,4);
                if(numWalls>=60)
                                {
                                        return 2;
                                }
                return regularValue;
        }
 
        public int PlaceWallLogic(int x,int y)
        {
                int numWalls = GetAdjacentWalls(x,y,1,1);
                int numWallsF = GetAdjacentWalls(x,y,2,2);
                //Winit(p) = rand(0,100) < 40
                //Repeat 4: W?(p) = R1(p) >= 5 || R2(p) == 2
                //Repeat 3: W?(p) = R1(p) >= 5
 
 
                if(Map[x,y]==1)
                {
                        if( numWalls >= 4 )
                        {
                                return 1;
                        }
                        if(numWalls<2)
                        {
                                return 0;
                        }
 
                }
                else
                {
                        if(bUse2Steps) {
                                if(numWalls>=5 || numWallsF==2)
                                {
                                        return 1;
                                }
                        } else {
                                if(numWalls>=5)
                                {
                                        return 1;
                                }
                        }
                }
                return 0;
        }
 
        public int GetAdjacentWalls(int x,int y,int scopeX,int scopeY)
        {
                int startX = x - scopeX;
                int startY = y - scopeY;
                int endX = x + scopeX;
                int endY = y + scopeY;
 
                int iX = startX;
                int iY = startY;
 
                int wallCounter = 0;
 
                for(iY = startY; iY <= endY; iY++) {
                        for(iX = startX; iX <= endX; iX++)
                        {
                                if(!(iX==x && iY==y))
                                {
                                        if(IsWall(iX,iY))
                                        {
                                                wallCounter += 1;
                                        }
                                }
                        }
                }
                return wallCounter;
        }
 
        /* bool hasWest = false;
        bool hasNorth = false;
        bool hasEast = false;
        bool hasSouth = false;*/
 
        public bool hasS(int x,int y)
        {
                if(IsWall(x,y-1)) {
                     return true;
                }
                return false;
        }
 
        public bool hasSW(int x,int y)
        {
                if(IsWall(x-1,y-1)) {
                     return true;
                }
                return false;
        }
 
        public bool hasSE(int x,int y)
        {
                if(IsWall(x+1,y-1)) {
                                return true;
                }
                return false;
        }
 
    	public bool hasE(int x,int y)
        {
                if(IsWall(x+1,y)) {
                                return true;
                }
                return false;
        }
 
    	public bool hasW(int x,int y)
        {
                if(IsWall(x-1,y)) {
                                return true;
                }
                return false;
        }
 
        public bool hasN(int x,int y)
        {
                if(IsWall(x,y+1)) {
                                return true;
                }
                return false;
        }
 
        public bool hasNW(int x,int y)
        {
                if(IsWall(x-1,y+1)) {
                                return true;
                }
                return false;
        }
 
        public bool hasNE(int x,int y)
        {
                if(IsWall(x+1,y+1)) {
                                return true;
                }
                return false;
        }
 
        bool IsWall(int x,int y)
        {
                // Consider out-of-bound a wall
                if( IsOutOfBounds(x,y) )
                {
                        return true;
                }
 
                if( Map[x,y]>=1  )
                {
                        return true;
                }
 
                if( Map[x,y]==0  )
                {
                        return false;
                }
                return false;
        }
 
        bool IsOutOfBounds(int x, int y)
        {
                if( x<0 || y<0 )
                {
                        return true;
                }
                else if( x>MapWidth-1 || y>MapHeight-1 )
                {
                        return true;
                }
                return false;
        }
 
        public void PrintMap()
        {
                //Console.Clear();
                MapToString();
        }
 
        public string MapToString()
        {
                string returnString = "";
                /*string returnString = string.Join(" ", // Seperator between each element
                                                  "Width:",
                                                  MapWidth.ToString(),
                                                  "\tHeight:",
                                                  MapHeight.ToString(),
                                                  "\t% Walls:",
                                                  PercentAreWalls.ToString(),
                                                  Environment.NewLine
                                                 );*/
 
                List<string> mapSymbols = new List<string>();
                mapSymbols.Add(".");
                mapSymbols.Add("#");
                mapSymbols.Add("+");
 
                for(int column=0,row=0; row < MapHeight; row++ ) {
                        for( column = 0; column < MapWidth; column++ )
                        {
                                returnString += mapSymbols[Map[column,row]];
                                //returnString += Map[column,row];
                        }
                        //returnString = "";
                        //returnString += Environment.NewLine;
                        returnString += "\n";
                }
                Debug.Log(returnString);
                CurrentMap=returnString;
                return returnString;
        }
 
        public void BlankMap()
        {
        	if (!Application.isPlaying){
        		BlankMapEditor();
        		return;
        	}
                for(int column=0,row=0; row < MapHeight; row++) {
                        for(column = 0; column < MapWidth; column++) {
                                Map[column,row] = 0;
                        }
                }
                // Reset rooms
                                Destroy(containerWallRooms);
                                Destroy(containerDecoration);
                                Destroy(containerEnemies);
                                Destroy(containerSpecialObjects);
                                Destroy(containerFilled);
                                SpawnedObjectsPos.Clear();
        }
        
        public void BlankMapEditor()
        {
        	foreach (Transform child in transform) {
			    GameObject.DestroyImmediate(child.gameObject,true);
			}
			foreach (Transform child in transform) {
			    GameObject.DestroyImmediate(child.gameObject,true);
			}
			foreach (Transform child in transform) {
			    GameObject.DestroyImmediate(child.gameObject,true);
			}
			DestroyImmediate(containerWallRooms,true);
            DestroyImmediate(containerDecoration,true);
            DestroyImmediate(containerEnemies,true);
            DestroyImmediate(containerSpecialObjects,true);
            DestroyImmediate(containerFilled,true);
        }
       
        public void RecalculateWaypoints(GameObject container) {

        }

        public void SpawnMap()
        {
                for(int column=0,row=0; row < MapHeight; row++) {
                        for(column = 0; column < MapWidth; column++) {
                                if(Map[column,row] == 1 && FilledSpaceArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(FilledSpaceArray[Random.Range(0,FilledSpaceArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 90, 0)) as GameObject;
                                        wall.transform.parent = containerFilled.transform;
                                }
                                if(Map[column,row] == 2 && CurvedOutsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedOutsideWallArray[Random.Range(0,CurvedOutsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, -90, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 3 && CurvedOutsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedOutsideWallArray[Random.Range(0,CurvedOutsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 90, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 4 && CurvedOutsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedOutsideWallArray[Random.Range(0,CurvedOutsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 0, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 5 && CurvedOutsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedOutsideWallArray[Random.Range(0,CurvedOutsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 180, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 6 && StraightWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(StraightWallArray[Random.Range(0,StraightWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 90, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 7 && StraightWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(StraightWallArray[Random.Range(0,StraightWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 270, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 8 && StraightWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(StraightWallArray[Random.Range(0,StraightWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 0, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 9 && StraightWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(StraightWallArray[Random.Range(0,StraightWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 180, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 10 && CurvedInsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedInsideWallArray[Random.Range(0,CurvedInsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 0, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 11 && CurvedInsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedInsideWallArray[Random.Range(0,CurvedInsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, -90, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 12 && CurvedInsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedInsideWallArray[Random.Range(0,CurvedInsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 90, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 13 && CurvedInsideWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(CurvedInsideWallArray[Random.Range(0,CurvedInsideWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 180, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                                if(Map[column,row] == 20 && ColumnWallArray.Length > 0) {
                                        GameObject wall = GameObject.Instantiate(ColumnWallArray[Random.Range(0,ColumnWallArray.Length)],new Vector3(column*tileSize,0.0f,row*tileSize),Quaternion.Euler(0, 0, 0)) as GameObject;
                                        wall.transform.parent = containerWallRooms.transform;
                                }
                        }
                }
                //container.transform.parent = containerRooms.transform;
                //RecalculateWaypoints(container);
        }
 
        public void RestartWallTiles()
        {
                for(int x=0,y=0; y < MapHeight; y++) {
                        for(x = 0; x < MapWidth; x++) {
                                if(Map[x,y] >= 1) {
                                        Map[x,y] = 1;
                                }
                        }
                }
        }
 
        public void ReplaceWallTiles()
        {
                for(int x=0,y=0; y < MapHeight; y++) {
                        for(x = 0; x < MapWidth; x++) {
                                if(Map[x,y] == 1) {
                                                int AdjWalls = GetAdjacentWalls(x,y,1,1);
                                                if(AdjWalls == 7) {
                                                        if(!hasNE(x,y)) {
                                                                //Is Pointing NE
                                                                Map[x,y] = 10;
                                                        }
                                                        if(!hasNW(x,y)) {
                                                                //Is Pointing NW
                                                                Map[x,y] = 11;
                                                        }
                                                        if(!hasSE(x,y)) {
                                                                //Is Pointing SE
                                                                Map[x,y] = 12;
                                                        }
                                                        if(!hasSW(x,y)) {
                                                                //Is Pointing SW
                                                                Map[x,y] = 13;
                                                        }
                                                }
                                                if(AdjWalls == 6) {
                                                                Map[x,y] = 20;
                                                }
                                                if(AdjWalls == 3) {
                                                                Map[x,y] = 20;
                                                }
                                                if(hasNE(x,y) && hasNW(x,y)) {
                                                        if(!hasS(x,y)) {
                                                                //IF IT'S WALL NOT CURVE POINTING SOUTH
                                                                Map[x,y] = 6;
                                                        }
                                                }
                                                if(hasSE(x,y) && hasSW(x,y)) {
                                                        if(!hasN(x,y)) {
                                                                //IF IT'S WALL NOT CURVE POINTING NORTH
                                                                Map[x,y] = 7;
                                                        }
                                                }
                                                if(hasNW(x,y) && hasSW(x,y)) {
                                                        if(!hasE(x,y)) {
                                                                //IF IT'S WALL NOT CURVE POINTING RIGHT
                                                                Map[x,y] = 8;
                                                        }
                                                }
                                                if(hasNE(x,y) && hasSE(x,y)) {
                                                        if(!hasW(x,y)) {
                                                                //IF IT'S WALL NOT CURVE POINTING Left
                                                                Map[x,y] = 9;
                                                        }
                                                }
                                                if(hasNE(x,y) || hasSW(x,y)) {
                                                        if(!hasNW(x,y) && !hasN(x,y) && !hasW(x,y)) {
                                                                //IF IT'S CURVE POINTING NORTH WEST
                                                                Map[x,y] = 2;
                                                        }
                                                        if(!hasE(x,y) && !hasSE(x,y) && !hasS(x,y)) {
                                                                //IF IT'S CURVE POINTING SOUTH EAST
                                                                Map[x,y] = 3;
                                                        }
                                                }
                                                if(!hasNE(x,y) && !hasSW(x,y)) {
                                                        if(!hasNW(x,y)) {
                                                                //IF IT'S CURVE POINTING NORTH WEST
                                                                Map[x,y] = 2;
                                                        }
                                                        if(!hasSE(x,y)) {
                                                                //IF IT'S CURVE POINTING SOUTH EAST
                                                                Map[x,y] = 3;
                                                        }
                                                }
                                                if(hasNW(x,y) || hasSE(x,y)) {
                                                        if(!hasNE(x,y) && !hasN(x,y) && !hasE(x,y)) {
                                                                //IF IT'S CURVE POINTING SOUTH WEST
                                                                Map[x,y] = 4;
                                                        }
                                                        if(!hasW(x,y) && !hasSW(x,y) && !hasS(x,y)) {
                                                                //IF IT'S CURVE POINTING NORTH EAST
                                                                Map[x,y] = 5;
                                                        }
                                                }
                                                if(!hasNW(x,y) && !hasSE(x,y) ) {
                                                        if(!hasNE(x,y)) {
                                                                //IF IT'S CURVE POINTING SOUTH WEST
                                                                Map[x,y] = 4;
                                                        }
                                                        if(!hasSW(x,y)) {
                                                                //IF IT'S CURVE POINTING NORTH EAST
                                                                Map[x,y] = 5;
                                                        }
                                                }
                                                if(AdjWalls == 0 || AdjWalls <= 2) {
                                                        Map[x,y] = 0;
                                                }
                                                if(AdjWalls == 3) {
                                                        Map[x,y] = 20;
                                                }
                                }
                        }
                }
        }
        
        public bool CanSpawnAt(Vector3 DesiredPosition) {
        	var checkResult = Physics.OverlapSphere( DesiredPosition, 0.005f);
			if (checkResult.Length > 2) {
			    return false;
			}
        	return true;
        }
       
        public void SpawnTreasure()
        {
			        while(ListEnemySpawnerCurrent.Count > 0) // Loop while there are coordinates to test
			        {
			        	if(EnemySpawnersArray.Length > 0) {
			        		Vector3 SpawnPosition = new Vector3( (float)ListEnemySpawnerCurrent[0].x * tileSize, 3.0f, (float)ListEnemySpawnerCurrent[0].y * tileSize );
			        		if(CanSpawnAt(SpawnPosition)) {
			        			
				                GameObject enemy = GameObject.Instantiate(EnemySpawnersArray[Random.Range(0,EnemySpawnersArray.Length)], SpawnPosition ,Quaternion.Euler(0, 0, 0)) as GameObject;
				                enemy.transform.parent = containerEnemies.transform;
				                SpawnedObjectsPos.Add(enemy.transform.position);
			                }
			            }
			            ListEnemySpawnerCurrent.RemoveAt(0);
			        }
			        while(ListDecorCurrent.Count > 0) // Loop while there are coordinates to test
			        {
			        		if(DecorArray.Length > 0) {
			        			Vector3 SpawnPosition = new Vector3( (float)ListDecorCurrent[0].x * tileSize, 0.0f, (float)ListDecorCurrent[0].y * tileSize );
			        			if(CanSpawnAt(SpawnPosition)) {
					                GameObject enemy = GameObject.Instantiate(DecorArray[Random.Range(0,DecorArray.Length)],SpawnPosition,Quaternion.Euler(0, Random.Range (-50, 50),0)) as GameObject;
					                enemy.transform.parent = containerDecoration.transform;
					                SpawnedObjectsPos.Add(enemy.transform.position);
				                }
			                }
			                ListDecorCurrent.RemoveAt(0);
			        }
			        while(ListSpecialObjects.Count > 0) // Loop while there are coordinates to test
			        {
			        		if(SpecialTreasureArray.Length > 0) {
				                if(Map[(int)ListSpecialObjects[0].x,(int)ListSpecialObjects[0].y] == -99) {
				                		Vector3 SpawnPosition = new Vector3( (float)ListSpecialObjects[0].x * tileSize, 0.0f, (float)ListSpecialObjects[0].y * tileSize );
				                		if(CanSpawnAt(SpawnPosition)) {
					                        GameObject enemy = GameObject.Instantiate(SpecialTreasureArray[Random.Range(0,SpecialTreasureArray.Length)],SpawnPosition,Quaternion.Euler(0, 0, 0)) as GameObject;
					                        enemy.transform.parent = containerSpecialObjects.transform;
					                        SpawnedObjectsPos.Add(enemy.transform.position);
				                        }
				                }
			                }
			                ListSpecialObjects.RemoveAt(0);
			        }
			       
			        while(ListTreasureObjectsCurrent.Count > 0) // Loop while there are coordinates to test
			        {
			        		if(TreasureArray.Length > 0) {
			        			Vector3 SpawnPosition = new Vector3( (float)ListTreasureObjectsCurrent[0].x * tileSize, 0.0f, (float)ListTreasureObjectsCurrent[0].y * tileSize );
			        			if(CanSpawnAt(SpawnPosition)) {
					                GameObject enemy = GameObject.Instantiate(TreasureArray[Random.Range(0,TreasureArray.Length)],SpawnPosition,Quaternion.Euler(0, 0, 0)) as GameObject;
					                enemy.transform.parent = containerSpecialObjects.transform;
					                SpawnedObjectsPos.Add(enemy.transform.position);
				                }
				            }
			                ListTreasureObjectsCurrent.RemoveAt(0);
			        }
			       
			        while(ListBreakableObjectsCurrent.Count > 0) // Loop while there are coordinates to test
			        {
			        		if(BreakableObjectsArray.Length > 0) {
			        			Vector3 SpawnPosition = new Vector3( (float)ListBreakableObjectsCurrent[0].x * tileSize, 0.0f, (float)ListBreakableObjectsCurrent[0].y * tileSize );			        			
			        			if(CanSpawnAt(SpawnPosition)) {
					                GameObject enemy = GameObject.Instantiate(BreakableObjectsArray[Random.Range(0,BreakableObjectsArray.Length)],SpawnPosition,Quaternion.Euler(0, 0, 0)) as GameObject;
					                enemy.transform.parent = containerSpecialObjects.transform;
					                SpawnedObjectsPos.Add(enemy.transform.position);
				                }
			                }
			                ListBreakableObjectsCurrent.RemoveAt(0);
			        }
			       
			        while(ListShipPartsCurrent.Count > 0) // Loop while there are coordinates to test
			        {
			        		if(GoalItemsArray.Length > 0) {
			        			Vector3 SpawnPosition = new Vector3( (float)ListShipPartsCurrent[0].x * tileSize, 0.0f, (float)ListShipPartsCurrent[0].y * tileSize );
			        			if(CanSpawnAt(SpawnPosition)) {
					                GameObject enemy = GameObject.Instantiate(GoalItemsArray[Random.Range(0,GoalItemsArray.Length)],SpawnPosition,Quaternion.Euler(0, 0, 0)) as GameObject;
					                enemy.transform.parent = containerSpecialObjects.transform;
					                SpawnedObjectsPos.Add(enemy.transform.position);
				                }
				            }
			                ListShipPartsCurrent.RemoveAt(0);
			        }
        }
       
        //public List<Vector2> ListDecorOptions = new List<Vector2>();
        //public List<Vector2> ListPlayerGoalOptions = new List<Vector2>();
        //public List<Vector2> ListBreakableObjectsOptions = new List<Vector2>();
        //public List<Vector2> ListEnemySpawnerOptions = new List<Vector2>();
                
        public void GetTreasureSpots()
        {
                for(int x=0,y=0; y < MapHeight; y++) {
                        for(x = 0; x < MapWidth; x++) {
                                if(!IsWall(x,y)) {
                                        if(GetAdjacentWalls(x,y,1,1) == 5) {
                                                //Debug.Log("Found a place to put a treasure");
                                                // REALLY RARE
                                                Map[x,y] = -4;
                                                ListSpecialObjects.Add(new Vector2(x, y));
                                        }
                                        if(GetAdjacentWalls(x,y,1,1) == 4) {
                                                //Debug.Log("Found a place to put a treasure");
                                                // REALLY RARE
                                                ListBreakableObjectsOptions.Add(new Vector2(x, y));
                                                ListTreasureObjectsOptions.Add(new Vector2(x, y));
                                        }
                                       
                                        if(GetAdjacentWalls(x,y,2,2) == 15 && Map[x,y] == 0) {
                                                //Debug.Log("Found a place to put a treasure");
                                                // Somewhat rare, but it's always there.
                                                //ListPlayerGoalOptions.Add(new Vector2(x, y));
                                                ListPlayerGoalOptions.Add(new Vector2(x, y));
                                                Map[x,y] = -3;
                                        }
                                               
                                        if( GetAdjacentWalls(x,y,3,3) <= 5 && GetAdjacentWalls(x,y,3,3) > 0  && Map[x,y] == 0) {
                                                //Find around edges - Good for regular encounters
                                                Map[x,y] = -2;
                                                ListEnemySpawnerOptions.Add(new Vector2(x, y));
                                                ListDecorOptions.Add(new Vector2(x, y));
                                        }
                                        if( GetAdjacentWalls(x,y,8,8) <= 22 && Map[x,y] == 0) {
                                                        //Find big areas Good for Big Encounters
                                                        Map[x,y] = -1;
                                                        ListEnemySpawnerOptions.Add(new Vector2(x, y));
                                                        ListShipPartsOptions.Add(new Vector2(x, y));
                                                        ListDecorOptions.Add(new Vector2(x, y));
                                        }
                                }
                        }
                }
               
            	if(ListPlayerGoalOptions.Count==0){
            		TryGenerationAgain();
            		return;
            	}
            		
                int randomPlayerPosition = Random.Range( 0, ListPlayerGoalOptions.Count-1 );
                int SpwnPlayer_x = (int)ListPlayerGoalOptions[randomPlayerPosition].x; // Find x value of coordinate to test
        		int SpwnPlayer_y = (int)ListPlayerGoalOptions[randomPlayerPosition].y;
 
        		int PerfectSpot_x = 0;
                int PerfectSpot_y = 0;
                float CurrentDistance = 0;
                float PrevDistance = 0;
                ListPlayerGoalOptions.RemoveAt (randomPlayerPosition);
               
        while(ListPlayerGoalOptions.Count > 0) // Loop while there are coordinates to test
        {  
            CurrentDistance = Vector2.Distance(new Vector2(SpwnPlayer_x, SpwnPlayer_y), new Vector2((int)ListPlayerGoalOptions[0].x, (int)ListPlayerGoalOptions[0].y));

            if( CurrentDistance > PrevDistance ) {
                PrevDistance = CurrentDistance;
                PerfectSpot_x = (int)ListPlayerGoalOptions[0].x;
                PerfectSpot_y = (int)ListPlayerGoalOptions[0].y;
            }

            if(PrevDistance == 0) { CurrentDistance = PrevDistance; }

            ListPlayerGoalOptions.RemoveAt (0); // Remove the currently tested coordinate
        }
            
        while(ListEnemySpawnerOptions.Count > 0 && EnemySpawnersArray.Length > 0) // Loop while there are coordinates to test
        {
                bool BadResult = false;
                if(ListEnemySpawnerCurrent.Count == 0) {
                        ListEnemySpawnerCurrent.Add(ListEnemySpawnerOptions[0]);
                        Map[(int)ListEnemySpawnerOptions[0].x,(int)ListEnemySpawnerOptions[0].y] = -99;
                        ListEnemySpawnerOptions.RemoveAt (0);
                }
               
                if(Map[(int)ListEnemySpawnerOptions[0].x,(int)ListEnemySpawnerOptions[0].y] == -99) {
                        ListEnemySpawnerOptions.RemoveAt (0);
                }
               
                for (int o = 0; o < ListEnemySpawnerCurrent.Count; o++) {              
                           if(Vector2.Distance(ListEnemySpawnerCurrent[o], ListEnemySpawnerOptions[0]) < EnemySpawnerDistance) {
                                        BadResult=true;
                           }
                        }
                       
                        if(!BadResult) {
                                ListEnemySpawnerCurrent.Add(ListEnemySpawnerOptions[0]);
                                Map[(int)ListEnemySpawnerOptions[0].x,(int)ListEnemySpawnerOptions[0].y] = -99;
                        }
                        ListEnemySpawnerOptions.RemoveAt(0);
                       
                //if(Vector2.Distance(ListEnemySpawnerCurrent[0], new Vector2((int)ListPlayerGoalOptions[0].x, (int)ListPlayerGoalOptions[0].y))
        }
       
        while(ListDecorOptions.Count > 0 && DecorArray.Length > 0) // Loop while there are coordinates to test
        {
                bool BadResult = false;
                if(ListDecorCurrent.Count == 0) {
                        //ListDecorCurrent.Add(ListDecorOptions[4]);
                        //ListDecorOptions.RemoveAt(4);  
                }
               
                if(Map[(int)ListDecorOptions[0].x,(int)ListDecorOptions[0].y] == -99) {
                        ListDecorOptions.RemoveAt (0);
                }
               
               if(ListDecorOptions.Count > 0) {    
            				for (int o = 0; o < ListDecorCurrent.Count; o++) {       
	                		  if(ListDecorOptions.Count > 0) {      
		                           if(Vector2.Distance(ListDecorCurrent[o], ListDecorOptions[0]) < DecorationSpawnerDistance) {
		                                        BadResult=true;
		                           }
	                           }
	                        }
	                       
	                        if(!BadResult && ListDecorOptions.Count > 0) {
	                                ListDecorCurrent.Add(ListDecorOptions[0]);
	                        }
	                        ListDecorOptions.RemoveAt(0);
	             }
                       
                //if(Vector2.Distance(ListDecorCurrent[0], new Vector2((int)ListPlayerGoalOptions[0].x, (int)ListPlayerGoalOptions[0].y))
        }
       
        while(ListTreasureObjectsOptions.Count > 0 && TreasureArray.Length > 0) // Loop while there are coordinates to test
        {
                bool BadResult = false;
                if(ListTreasureObjectsCurrent.Count == 0) {
                        ListTreasureObjectsCurrent.Add(ListTreasureObjectsOptions[0]);
                        Map[(int)ListTreasureObjectsOptions[0].x,(int)ListTreasureObjectsOptions[0].y] = -99;
                        ListTreasureObjectsOptions.RemoveAt (0);
                }
               
                if(Map[(int)ListTreasureObjectsOptions[0].x,(int)ListTreasureObjectsOptions[0].y] == -99) {
                        ListTreasureObjectsOptions.RemoveAt (0);
                }
               
                for (int o = 0; o < ListTreasureObjectsCurrent.Count; o++) {           
                           if(Vector2.Distance(ListTreasureObjectsCurrent[o], ListTreasureObjectsOptions[0]) < TreasureObjSpawnerDistance) {
                                        BadResult=true;
                           }
                        }
                       
                        if(!BadResult) {
                                ListTreasureObjectsCurrent.Add(ListTreasureObjectsOptions[0]);
                                Map[(int)ListTreasureObjectsOptions[0].x,(int)ListTreasureObjectsOptions[0].y] = -99;
                        }
                        ListTreasureObjectsOptions.RemoveAt(0);
        }
       
        while(ListBreakableObjectsOptions.Count > 0 && BreakableObjectsArray.Length > 0) // Loop while there are coordinates to test
        {
                bool BadResult = false;
                if(ListBreakableObjectsCurrent.Count == 0) {
                        ListBreakableObjectsCurrent.Add(ListBreakableObjectsOptions[0]);
                        Map[(int)ListBreakableObjectsOptions[0].x,(int)ListBreakableObjectsOptions[0].y] = -99;
                        ListBreakableObjectsOptions.RemoveAt (0);
                }
               
                if(Map[(int)ListBreakableObjectsOptions[0].x,(int)ListBreakableObjectsOptions[0].y] == -99) {
                        ListBreakableObjectsOptions.RemoveAt (0);
                }
               
            	if(ListBreakableObjectsOptions.Count>0) {
                		for (int o = 0; o < ListBreakableObjectsCurrent.Count; o++) {
                		   if(ListBreakableObjectsOptions.Count>0) {
	                           if(Vector2.Distance(ListBreakableObjectsCurrent[o], ListBreakableObjectsOptions[0]) < BreakableObjSpawnerDistance) {
	                                        BadResult=true;
	                           }
	                       }
                        }
                       
                        if(!BadResult && ListBreakableObjectsOptions.Count>0) {
                                ListBreakableObjectsCurrent.Add(ListBreakableObjectsOptions[0]);
                                Map[(int)ListBreakableObjectsOptions[0].x,(int)ListBreakableObjectsOptions[0].y] = -99;
                        }
                        ListBreakableObjectsOptions.RemoveAt(0);
                }
        }
       
       
        while(ListShipPartsOptions.Count > 0 && GoalItemsArray.Length > 0) // Loop while there are coordinates to test
        {
                bool BadResult = false;
                if(ListShipPartsCurrent.Count == 0) {
                        ListShipPartsCurrent.Add(ListShipPartsOptions[0]);
                        Map[(int)ListShipPartsOptions[0].x,(int)ListShipPartsOptions[0].y] = -99;
                        ListShipPartsOptions.RemoveAt (0);
                }
               
                if(Map[(int)ListShipPartsOptions[0].x,(int)ListShipPartsOptions[0].y] == -99) {
                        ListShipPartsOptions.RemoveAt (0);
                }
               
                for (int o = 0; o < ListShipPartsCurrent.Count; o++) {         
                           if(Vector2.Distance(ListShipPartsCurrent[o], ListShipPartsOptions[0]) < ShipPartsSpawnerDistance) {
                                        BadResult=true;
                           }
                        }
                       
                        if(!BadResult) {
                                ListShipPartsCurrent.Add(ListShipPartsOptions[0]);
                                Map[(int)ListShipPartsOptions[0].x,(int)ListShipPartsOptions[0].y] = -99;
                        }
                        ListShipPartsOptions.RemoveAt(0);
        }
       
        SpawnPlayerAndGoal(SpwnPlayer_x,SpwnPlayer_y,PerfectSpot_x,PerfectSpot_y);
    }
 
        public void SpawnPlayerAndGoal(int px, int py, int gx, int gy) {
                Map[px,py]=-99;
                Map[gx,gy]=-99;
                
                if(px==0||py==0||gx==0||gy==0) {
                	TryGenerationAgain();
                	return;
                }
                
                if(SpawnPoint!=null) {
                        SpawnPoint.transform.position = new Vector3(px*tileSize,3.0f,py*tileSize);
                        //player.GetComponent<PlayerMove>().ResetRespawnPoint();
                        SpawnedObjectsPos.Add(SpawnPoint.transform.position);
                }
                if(GoalLevel!=null) {
                	if(goalspawned)
                		return;
	                GameObject wall2 = GameObject.Instantiate(GoalLevel,new Vector3(gx*tileSize,0.0f,gy*tileSize),Quaternion.Euler(0, 0, 0)) as GameObject;
	                //Debug.Log("Goal Spot is x "+gx+" y "+gy,this);
	                wall2.transform.parent = containerSpecialObjects.transform;
	                goalspawned=true;
	                SpawnedObjectsPos.Add(wall2.transform.position);
                }
        }
 
        public void RandomFillMap()
        {
                // New, empty map
                Map = new int[MapWidth,MapHeight];
 
                int mapMiddle = 0; // Temp variable
                for(int column=0,row=0; row < MapHeight; row++) {
                        for(column = 0; column < MapWidth; column++)
                        {
                                // If coordinants lie on the the edge of the map (creates a border)
                                if(column == 0 || column == 1 || column == 2 || column == 3 || column == 4)
                                {
                                        Map[column,row] = 1;
                                }
                                else if (row == 0 || row == 1 || row == 2 || row == 3 )
                                {
                                        Map[column,row] = 1;
                                }
                                else if (column == MapWidth-1 || column == MapWidth-2 || column == MapWidth-3  || column == MapWidth-4 )
                                {
                                        Map[column,row] = 1;
                                }
                                else if (row == MapHeight-1 || row == MapHeight-2 || row == MapHeight-3 || row == MapHeight-4)
                                {
                                        Map[column,row] = 1;
                                }
                                // Else, fill with a wall a random percent of the time
                                else
                                {
                                        mapMiddle = (MapHeight / 2);
 
                                        if(row == mapMiddle)
                                        {
                                                Map[column,row] = 0;
                                        }
                                        else
                                        {
                                                Map[column,row] = RandomPercent(PercentAreWalls);
                                        }
                                }
                        }
                }
        }
 
        int RandomPercent(int percent)
        {
                if(percent>=Random.Range(1,101))
                {
                        return 1;
                }
                return 0;
        }
 
        //public void CreateDungeonHalls(int[,] _dungeonMap, int _sizeX,int _sizeZ, int nbrRoom)       
        public void MakeConnections() {
                FindRooms(Map,MapWidth,MapHeight);
        }
       
        public void NormalizeRooms() {
                int emptySpaceInt = maxIndex+10;
               
                for(int column=0,row=0; row < MapHeight; row++) {
                        for(column = 0; column < MapWidth; column++) {
                                if(Map[column,row] > 1) {
                                        if(Map[column,row] != emptySpaceInt) {
                                                Map[column,row] = 1;
                                        } else {
                                                Map[column,row] = 0;
                                        }
                                }
                        }
                }
        }
       
        // Loop through the dungeon map and find rooms created by touching squares
        public void FindRooms(int[,] _dungeonMap, int _sizeX, int _sizeZ)
        {
             
            List<Vector2> ListCoordToTest = new List<Vector2>(); // List of all coordinate that need to be evaluated for the current room.
            int[,] _modifiedMap  = new int[_sizeX,_sizeZ]; // This array will be a copy of _modifiedMap where all known room cell are set as 0 to be ignored by the next steps
            int _nbrRoomFound = 0;
            int[] myRooms = new int[100];
            int maxValue = 0;
             
            System.Array.Copy(_dungeonMap,_modifiedMap, _sizeX*_sizeZ); // Create a copy of the _dungeonMap in _modifiedMap
             
            //Loop through all position of the map
            for(int i = 0; i < _sizeX; i++)
            {
                for(int j = 0; j < _sizeZ; j++)
                {
                    // If a room is found
                    if(_modifiedMap[i,j] == 0)
                    {
                        ListCoordToTest.Add(new Vector2(i, j)); // Add the coordinate to the list to test
                         
                        while(ListCoordToTest.Count > 0) // Loop while there are coordinates to test
                        {  
                            int _x = (int)ListCoordToTest[0].x; // Find x value of coordinate to test
                            int _z = (int)ListCoordToTest[0].y; // Find y value of coordinate to test
                            ListCoordToTest.RemoveAt (0); // Remove the currently tested coordinate
                             
                            //_dungeonMap[_x,_z] = _nbrRoomFound + 1; // Update the _dungeonMap with the current number of room found
                             
                            // Look the 8 coordinate around the current coordinate to find for connected rooms
                            for(int _xAround = _x - 1; _xAround <= _x + 1; _xAround++)
                            {
                                for(int _zAround = _z - 1 ; _zAround <= _z + 1; _zAround++)
                                {
                                       if(_xAround > 0 && _zAround > 0 && _xAround < _sizeX + 1 && _zAround < _sizeZ + 1) {
                                            // Test if evaluated coordinate is a square and need to be added to the room
                                            if(_modifiedMap[_xAround,_zAround] == 0)
                                            {
                                                        ListCoordToTest.Add(new Vector2(_xAround, _zAround)); // Add it to the list of coordinates to test
                                                        _modifiedMap[_xAround,_zAround] = _nbrRoomFound+10; // Remove the room position from the modified map so we don't step on it again
                                                        myRooms[_nbrRoomFound]++;
                                            }
                                       }
                                }
                            }
                        }
                        _nbrRoomFound++;
                    }
                }
            }
           
            for(int i=0;i<_nbrRoomFound;i++){
                 if(myRooms[i]>maxValue){
                  maxValue=myRooms[i];
                  maxIndex=i;
                 }
        }
       
        for(int column=0,row=0; row < MapHeight; row++) {
                        for(column = 0; column < MapWidth; column++) {
                                Map[column,row] = _modifiedMap[column,row];
                        }
                }
           
            //return _nbrRoomFound;
        }
 
 
        public void MapHandler(int mapWidth, int mapHeight, int[,] map, int percentWalls=40)
        {
                this.MapWidth = mapWidth;
                this.MapHeight = mapHeight;
                this.PercentAreWalls = percentWalls;
                this.Map = new int[this.MapWidth,this.MapHeight];
                this.Map = map;
        }
       
        // On Start
        void Start ()
        {
        	if(!GameManager.instance.useSeed)
            	StartCoroutine(SaySomeThings());
            if(GameManager.instance.Loading) {
            	GameManager.instance.Loading=false;
            }
        }
        
        public void TryGenerationAgain() {
        	Debug.Log("No perfect spot for goal or player, regenerating",this);
        	badresult++;
        	StopAllCoroutines();
        	if(badresult>=10) {
        		Debug.Log("Tried 10 times, it's impossible to find a player spot and goal spot under these settings");
        		badresult=0;
        		return;
        	}
        	if(useSeed) {
        		useSeed=false;
        		DoGenerate();
        		StartCoroutine(GoBackAtUsingSeed());
        	} else {
        		DoGenerate();
        	}
        }
        
        IEnumerator GoBackAtUsingSeed()
	    {
	        yield return 15;
	        useSeed=true;
	    }
 
		IEnumerator SaySomeThings()
	    {
	    	 if(useSeed)
	    		UsedSeed=Seed;
	    	 else
	    		UsedSeed=System.Environment.TickCount;
	    	
	    	Random.seed=UsedSeed;
	    	
	         bUse2Steps=true;
            	MapHandler();  
            	MakeCaverns();
            	MakeCaverns();
            	MakeCaverns();
            	MakeCaverns();
	        yield return 0;
	        	bUse2Steps=false;
            	MakeCaverns();
            	MakeCaverns();
            	MakeCaverns();
            	MakeConnections();
	        yield return 0;
	        	NormalizeRooms();
            	ReplaceWallTiles();
	        	GetTreasureSpots();
	        yield return 0;
            	SpawnMap();
            yield return 0;
				SpawnTreasure();
			if(SpawnPlayers && Application.isPlaying)
				GameManager.instance.spawnPlayers();
							
			badresult=0;
			Random.seed=System.Environment.TickCount;
	    }
	    
	    IEnumerator SaySomeThingsEditor()
	    {
	    	 if(useSeed)
	    		UsedSeed=Seed;
	    	 else
	    		UsedSeed=System.Environment.TickCount;
	    	
	    	Random.seed=UsedSeed;
	    	
	        bUse2Steps=true;
        	MapHandler();  
        	MakeCaverns();
        	MakeCaverns();
        	MakeCaverns();
        	MakeCaverns();
        	bUse2Steps=false;
        	MakeCaverns();
        	MakeCaverns();
        	MakeCaverns();
        	MakeConnections();
        	NormalizeRooms();
        	ReplaceWallTiles();
        	GetTreasureSpots();
        	SpawnMap();
			SpawnTreasure();
							
			badresult=0;
			Random.seed=System.Environment.TickCount;
			yield return 0;
	    }
	 
        
        public void DoGenerate() {
        		if(Application.isPlaying) {
                	BlankMap();
                	StartCoroutine(SaySomeThings());
                } else {
                	BlankMapEditor();
                	StartCoroutine(SaySomeThingsEditor());
                }
        }
        
        public void EditorDoGenerate() {
        		BlankMapEditor();
                StartCoroutine(SaySomeThingsEditor());
        }
       
}