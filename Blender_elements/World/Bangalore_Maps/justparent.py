import bpy

cuts = 36
objects = bpy.data.objects
bpy.ops.wm.save_as_mainfile(filepath="C:\\Users\\vibe\\Desktop\\KARGAME\\MASTER-roads.blend")
for i in range(1,cuts+1):
    bpy.ops.wm.save_as_mainfile(filepath="C:\\Users\\vibe\\Desktop\\KARGAME\\MASTER-roads.blend")
    bpy.ops.object.select_all(action='DESELECT')
    for j in range(1,cuts+1):
        #print("map_4_"+str(i-1)+"_"+str(j-1))
        b = objects["Collider_4_"+str(j-1)+"_"+str(i-1)]
        a =objects["roads_4_"+str(j-1)+"_"+str(i-1)]
        b.select_set(True)
        a.select_set(True)
        bpy.context.view_layer.objects.active = a
        bpy.ops.object.parent_set()
        a.select_set(False)
        b.select_set(False)
        bpy.context.view_layer.objects.active = None
        print(str(i-1) + "," + str(j-1))
        