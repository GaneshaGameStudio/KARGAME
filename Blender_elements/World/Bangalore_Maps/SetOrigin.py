import bpy

for collection in bpy.data.collections:
       if(collection.name == "Collection"):
           for obj in collection.all_objects:
              print('   obj: ', obj.name)
              bpy.data.objects[obj.name].select_set(True)
              bpy.ops.object.origin_set(type='ORIGIN_CENTER_OF_VOLUME', center='MEDIAN')
              bpy.ops.object.select_all(action='DESELECT')
