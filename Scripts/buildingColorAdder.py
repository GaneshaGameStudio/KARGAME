from xml.etree import ElementTree
import xml.etree.ElementTree as et
import random

buildingColors = ("#FFBBAD","#DFBFDE","#F4E9AC","#A6E0F8","#698A7D","#FF934D","#BB9BBA","#D8CE97","#6CA3BA")

# actual map.osm data from Openstreetmap which is imported through blender - use this as input file
tree = et.parse("F:\Blender\YashaswiOSM\map_5.osm")
root = tree.getroot()

count = 0
for e in tree.findall('.//way'):
    count += 1
    print(e.tag +" - "+ count.__str__())

    tagSize = len(e.findall("./tag[@k='building']"))
    if tagSize==1:
        colorSize = len(e.findall("./tag[@k='building:colour']"))
        if colorSize==0:
            #generate random number and get the random color from the tuple
            rand = random.randint(0,8)
            color = buildingColors[rand]

            thisdict = {"k":"building:colour","v":color}
            ElementTree.SubElement(e,"tag",thisdict)

# uncomment below line to print the edited xml to console
# et.dump(root)

# writes it as a new file
tree.write("F:\Blender\YashaswiOSM\Output\map5.xml")