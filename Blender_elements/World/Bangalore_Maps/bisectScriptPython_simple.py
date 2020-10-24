import bpy,sys
import bmesh

cuts = 3
ycoordList = []
xcoordList = []
obj = bpy.context.active_object
v = obj.data.vertices[0]

coords = [(obj.matrix_world @ v.co) for v in obj.data.vertices]
for ycoord in coords:
    ycoordList.append(ycoord[1])
for xcoord in coords:
    xcoordList.append(xcoord[0])

ymin = min(ycoordList)
ymax = max(ycoordList)
xmin = min(xcoordList)
xmax = max(xcoordList)

yinterval = abs(ymin - ymax) / cuts
xinterval = abs(xmin - xmax) / cuts
j=0
def MakeSplit(name,interval,normal,left,right,l,axisenter,cuts):
    for i in range(0,cuts-1):
        bpy.ops.object.mode_set(mode="EDIT")
        bpy.ops.mesh.select_all(action="SELECT")
        if(axisenter=='Y'):
            plane_co=(0,interval*(i+1) + left, 0)
        else:
            plane_co=(interval*(i+1) + left, 0,0)
        bpy.ops.mesh.bisect(plane_co=plane_co, plane_no=normal)
        mesh = bmesh.from_edit_mesh(bpy.context.object.data)
        k=0
        for v in mesh.verts:
            if(v.select == True):
                if(k==0):
                    v.select = True
                    mesh.select_history.add(v)
                else:
                    v.select = False
                k=k+1
        bpy.ops.mesh.select_axis(axis=axisenter)    
        
        bpy.ops.mesh.separate(type='SELECTED')
        bpy.ops.object.mode_set(mode="OBJECT")
        bpy.ops.object.select_all(action='DESELECT')
        bpy.data.objects[name].select_set(True)
        print(name)
        if(l==0):
            bpy.context.selected_objects[0].name = "map_4_" + str(i) + "_" + str(j) 
        else:
            bpy.context.selected_objects[0].name = "map_4_" +  str(j) + "_" + str(i) 
        bpy.ops.object.select_all(action='DESELECT')
        bpy.data.objects[name + ".001"].select_set(True) 
        name = "Plane"
        bpy.context.selected_objects[0].name = name
        ob = bpy.context.scene.objects[name]
        bpy.context.view_layer.objects.active = ob 
    if(l==0):
        bpy.context.selected_objects[0].name = "map_4_" + str(cuts-1) + "_" + str(j)
    else:
        bpy.context.selected_objects[0].name = "map_4_" +  str(j) + "_" + str(cuts-1)

MakeSplit("Plane",yinterval,(0,1,0),ymin,ymax,0,'Y',cuts)

for j in range(0,cuts):
    newname = "map_4_" + str(j) + "_0"
    o = bpy.context.scene.objects["map_4_" + str(j) + "_0"]
    bpy.context.view_layer.objects.active = o
    bpy.ops.object.select_all(action='DESELECT')
    bpy.data.objects["map_4_" + str(j) + "_0"].select_set(True)
    MakeSplit("map_4_" + str(j) + "_0",xinterval,(1,0,0),xmin,xmax,1,'X',cuts)