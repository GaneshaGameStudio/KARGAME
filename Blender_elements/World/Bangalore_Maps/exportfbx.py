import bpy

for i in range(0,35):
    for j in range(0,35):   
        bpy.ops.object.select_all(action='DESELECT')
        a =bpy.context.scene.objects["map_4_" + str(i) + "_" + str(j)] 
        bpy.context.view_layer.objects.active = a
        #a.select_set(True)
        bpy.ops.object.select_grouped(type='CHILDREN_RECURSIVE') 
        a.select_set(True) 
        
        bpy.ops.export_scene.fbx(filepath="/Users/vishakhbegari/Documents/GGS/KARGAME/Blender_elements/World/Bangalore_Maps/export/Map/map_4_"+str(i)+"_"+str(j)+".fbx",filter_glob='*.fbx', use_selection=True, use_active_collection=True,apply_unit_scale=True, apply_scale_options='FBX_SCALE_UNITS', object_types={'MESH'})
        print(str(i) + "," + str(j))