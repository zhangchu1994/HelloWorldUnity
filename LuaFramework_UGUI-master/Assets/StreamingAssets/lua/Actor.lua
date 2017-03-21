require "Common/define"

Actor = {};
local this = Actor;

local panel;
local prompt;
local transform;
local gameObject;

function Actor.New()
    return this;
end

function Actor.Awake()
    log("Actor.Awake______________________________");
end

function Actor.Start()
    log("Actor.Start______________________________");
end

function Actor.Show()
    log("Actor.Show__________________________________");

    -- local index = index;
    -- local skeleton = "ch_pc_hou";
    -- local head = "ch_pc_hou_".."004".."_tou";
    -- local chest = "ch_pc_hou_".."004".."_shen";
    -- local hand = "ch_pc_hou_".."004".."_shou";
    -- local feet = "ch_pc_hou_".."004".."_jiao";

    -- -- //Creates the skeleton object
    -- local res = Resources.Load ("Actor/Actor1/"..skeleton);
    -- local skeletonObj = GameObject.Instantiate (res);

    -- -- "ch_we_one_hou_" + index[DEFAULT_WEAPON],
             
    -- local equipments = {};-- = new string[4];
    -- equipments [1] = head;
    -- equipments [2] = chest;
    -- equipments [3] = hand;
    -- equipments [4] = feet;
        
    -- -- // Create and collect other parts SkinnedMeshRednerer
    -- local meshes = {};--SkinnedMeshRenderer.New(4)
    -- local objects = {};--GameObject.New(4);
    
    -- for i = 1, #equipments do 
    --     res = Resources.Load ("Actor/Actor1/"..equipments [i]);
    --     objects[i] = GameObject.Instantiate (res);
    --     meshes[i] = objects[i].GetComponentInChildren(objects[i],typeof(SkinnedMeshRenderer));
    --                 -- meshes[i] = objects[i].GetComponentInChildren<SkinnedMeshRenderer> ();
    -- end  

    -- -- -- // Combine meshes
    -- this.CombineObject (skeletonObj, meshes, true);

    -- -- // Delete temporal resources
    -- for (int i = 0; i < objects.Length; i++) {
        
    --     GameObject.DestroyImmediate (objects [i].gameObject);
    -- }
        
    -- // Create weapon
    -- res = Resources.Load ("Prefab/" + weapon);
    -- WeaponInstance = GameObject.Instantiate (res) as GameObject;
    
    -- Transform[] transforms = Instance.GetComponentsInChildren<Transform>();
    -- foreach (Transform joint in transforms) {
    --     if (joint.name == "weapon_hand_r") {// find the joint (need the support of art designer)
    --         WeaponInstance.transform.parent = joint.gameObject.transform;
    --         break;
    --     }   
    -- }

    -- // Init weapon relative informations
    -- WeaponInstance.transform.localScale = Vector3.one;
    -- WeaponInstance.transform.localPosition = Vector3.zero;
    -- WeaponInstance.transform.localRotation = Quaternion.identity;

    -- // Only for display
    -- animationController = Instance.GetComponent<Animation>();
end

function Actor.CombineObject(skeleton, meshes,combine )
    -- // Fetch all bones of the skeleton
    local transforms = {};
    local skeletonTransform = skeleton.GetComponentsInChildren(skeleton,typeof(Transform),true);
    addArrayRange(transforms,skeletonTransform)

    local materials = {};--Material//the list of materials
    local combineInstances = {};--CombineInstance//the list of meshes
    local bones = {};--Transform //the list of bones

    -- -- // Below informations only are used for merge materilas(bool combine = true)
    local oldUV = nil;--List<Vector2[]>
    local newMaterial = nil;--Material
    local newDiffuseTex = nil;--Texture2D

    --Collect information from meshes
    for i = 1, #meshes do
        local smr = meshes[i];
        addArrayRange(materials,smr.materials); --// Collect materials
        -- print("__________111_"..i);
        -- printArray(materials);
        -- -- // Collect meshes
        for sub=1,smr.sharedMesh.subMeshCount do
            local ci =  CombineInstance.New();
            ci.mesh = smr.sharedMesh;
            ci.subMeshIndex = sub;
            table.insert(combineInstances, ci)
            -- print("__________222_"..sub.."_"..smr.sharedMesh.subMeshCount);
        end

        -- print("CombineObject_________ = "..i.." length = "..smr.bones.Length.." transforms = "..tostring(#transforms));
        -- -- // Collect bones
        for j = 1 , smr.bones.Length do
            for tBase = 1, #transforms do
                if smr.bones[j-1].name == transforms[tBase].name then
                    table.insert(bones,transforms[tBase]);
                    break;
                end
            end
        end
    end

    -- // merge materials
    if combine then
        newMaterial = Material.New(Shader.Find ("Mobile/Diffuse"));
        oldUV = {};--new List<Vector2[]>()
        
        -- // merge the texture
        local Textures = {};
        for i = 1, #materials do
            Textures[i] =  materials[i].GetTexture(materials[i],"_MainTex");--.Add(materials[i].GetTexture(COMBINE_DIFFUSE_TEXTURE) as Texture2D);
        end

        newDiffuseTex = Texture2D.New(512, 512, TextureFormat.RGBA32, true);
        local uvs = newDiffuseTex.PackTextures(newDiffuseTex,Textures, 0);--Rect[]
        newMaterial.mainTexture = newDiffuseTex;

        -- // reset uv
        -- local uva, uvb;--Vector2[]
        -- for j = 1, #combineInstances do
        --     uva = combineInstances[j].mesh.uv;
        --     uvb = Vector2.New(uva.Length);
        --     for k = 1, uva.Length do
        --         uvb[k-1] = Vector2.New((uva[k-1].x * uvs[j-1].width) + uvs[j-1].x, (uva[k-1].y * uvs[j-1].height) + uvs[j-1].y);
        --     end
        --     oldUV[j] = combineInstances[j].mesh.uv;
        --     combineInstances[j].mesh.uv = uvb;
        -- end
        -- print("_______________________ j = "..j.." uv = "..uvb.Length);
        -- for k,v in pairs(uvb) do
        --     print(k,v)
        -- end
    end
    


    -- -- -- // Create a new SkinnedMeshRenderer
    -- local oldSKinned = skeleton:GetComponent('SkinnedMeshRenderer');
    -- if oldSKinned ~= nil then
    --     GameObject.DestroyImmediate (oldSKinned);
    -- end
    -- local r = skeleton:AddComponent(typeof(SkinnedMeshRenderer));
    -- r.sharedMesh = Mesh.New();
    -- -- r.sharedMesh.CombineMeshes(combineInstances.ToArray(), combine, false);--// Combine meshes
    -- r.bones = bones.ToArray();--// Use new bones
    -- if combine then
    --     r.material = newMaterial;
    --     for i = 1 ,combineInstances.Count do
    --         combineInstances[i-1].mesh.uv = oldUV[i-1];
    --     end
    -- else
    --     r.materials = materials.ToArray();
    -- end
end 



function Actor.OnCreate(obj)
    -- log('Actor.OnCreate____________');
    gameObject = obj;
    transform = obj.transform;

    panel = transform:GetComponent('loginPanel');
    prompt = transform:GetComponent('LuaBehaviour');

    local loginButton = transform:FindChild("Login").gameObject;

    prompt:AddClick(loginButton, this.OnClick);
    -- resMgr:LoadPrefab('prompt', { 'PromptItem' }, this.InitPanel);
end

-- --初始化面板--
-- function Login.InitPanel(objs)
--     local count = 100; 
--     local parent = PromptPanel.gridParent;
--     for i = 1, count do
--         local go = newObject(objs[0]);
--         go.name = 'Item'..tostring(i);
--         go.transform:SetParent(parent);
--         go.transform.localScale = Vector3.one;
--         go.transform.localPosition = Vector3.zero;
--         prompt:AddClick(go, this.OnItemClick);

--         local label = go.transform:FindChild('Text');
--         label:GetComponent('Text').text = tostring(i);
--     end
-- end

-- --滚动项单击--
-- function Login.OnItemClick(go)
--     log(go.name);
-- end

--单击事件--
function Actor.OnClick(go)
    sceneMgr:GoToScene('maincityscene',"MainCity","MainCityScene",this.SceneDone);  
end

function Actor.SceneDone(obj)

end

--关闭事件--
function Actor.Close()
    panelMgr:ClosePanel(CtrlNames.Prompt);
end


