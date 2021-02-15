import os
import bpy
import csv

filepath = bpy.data.filepath
print(filepath)
directory = os.path.dirname(filepath) + "/OSMroutes_parser/bus_routes/"
for filename in os.listdir(directory):
    if filename.endswith(".csv"): 
        print(filename.split(".")[0])
        #create parent object here
        if not(bpy.context.scene.objects.get(filename.split(".")[0])):
            print(filename.split(".")[0])
            bpy.ops.mesh.primitive_cube_add()
            par = bpy.context.selected_objects[0]
            # change name
            par.name = filename.split(".")[0]
            bpy.ops.object.mode_set(mode="EDIT")
            bpy.ops.mesh.select_all(action="SELECT")
            bpy.ops.mesh.delete(type='VERT')
            bpy.ops.object.mode_set(mode="OBJECT")
            i = 0
            with open(directory + filename, "rt", encoding='ascii') as infile:
                read = csv.reader(infile)
                for row in read :
                    bpy.ops.mesh.primitive_cube_add()
                    chld = bpy.context.selected_objects[0]
                    chld.name = filename.split(".")[0] + "-Cube" + str(i)
                    chld.location =(float(row[0]),float(row[1]),0)
                    chld.parent = par
                    bpy.ops.object.select_all(action='DESELECT')
                    i+=1
    else:
        continue
    
    #break