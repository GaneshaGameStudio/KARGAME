import bpy
import csv

cuts = 36

ymin = -3323
ymax = 3323
xmin = -3698
xmax = 3698
yinterval = abs(ymin - ymax) / cuts
xinterval = abs(xmin - xmax) / cuts

ymini = ymin
xmini = xmin
objects = bpy.data.objects
from mathutils import Vector


# checks if a supplied coordinate if in the bounding box created by vector1 and vector2
# vector_check           the vector which is compared against the bounding box
# vector1                     the vector defining the start of the bounding box
# vector2                     the vector defining the end of the bounding box
def IsInBoundingVectors(vector_check, vector1, vector2):
    # if vector_check is either bigger or smaller than both other, it does not lie between them
    # in that case it won't be inside the bounding box; hence return false
    for i in range(0, 3):
        if (vector_check[i] < vector1[i] and vector_check[i] < vector2[i]
            or vector_check[i] > vector1[i] and vector_check[i] > vector2[i]):
            return False
    return True



def SelectObjectsInBound(vector1, vector2):
    # deselect all
    bpy.ops.object.select_all(action='DESELECT')

    # cycle through all objects in the scene
    for obj in bpy.context.scene.objects:
        # check if the object is in the bounding vectors
        # if yes, select it
        # if no, deselect it
        if(IsInBoundingVectors(obj.matrix_world.to_translation(), vector1, vector2)):
            if(obj.users_collection[0].name == "Collection"):
                obj.select_set(True)   
        else:
            obj.select_set(False)
#bpy.ops.wm.save_as_mainfile(filepath="C:\\Users\\vibe\\Desktop\\KARGAME\\MASTER.blend")
with open('Map4_Busroute_tracker.csv','w') as csvfile:
    for i in range(1,cuts+1):
        #bpy.ops.wm.save_as_mainfile(filepath="C:\\Users\\vibe\\Desktop\\KARGAME\\MASTER.blend")
        bpy.ops.object.select_all(action='DESELECT')
        
        for j in range(1,cuts+1):
            if(j==17 or j==18):
                csvfile.write(str(i-1) + ' , '+str(j-1) + ' ,\n ')
                continue
            
            bpy.ops.object.select_all(action='DESELECT')
            csvfile.write(str(i-1) + ' , '+str(j-1) + ' , ')
            SelectObjectsInBound(Vector((xmini, ymini, -1000)), Vector((xmini + xinterval, ymini + yinterval , 1000)))
            selection_names = [obj.name for obj in bpy.context.selected_objects]
           
            for g in selection_names:
                v = bpy.data.objects[g].parent
                csvfile.write(v.name + ' , ')
                
            csvfile.write('\n')
            ymini = ymin+(j*yinterval)
            
            
            bpy.context.view_layer.objects.active = None
            print(str(i-1) + "," + str(j-1))
        xmini = xmin+(i*xinterval)
        ymini = ymin
csvfile.close()
        