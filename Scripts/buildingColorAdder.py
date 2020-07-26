import sys
import os
from xml.etree import ElementTree
import xml.etree.ElementTree as et
import random

buildingColors = ("#FFBBAD","#DFBFDE","#F4E9AC","#A6E0F8","#698A7D","#FF934D","#BB9BBA","#D8CE97","#6CA3BA","#ECECEC","#8BAA4E")
roofColors = ("#93897E","#CACACA","#848484")

# actual map.osm data from Openstreetmap which is imported through blender - use this as input file below
tree = et.parse(sys.path[0]+"\map_5.osm")
root = tree.getroot()

count = 0
for e in tree.findall('.//way'):
    count += 1
    print(e.tag +" - "+ count.__str__())

    tagSize = len(e.findall("./tag[@k='building']"))
    if tagSize==1:
        colorSize = len(e.findall("./tag[@k='building:colour']"))
        if colorSize==0:
            #generate random number and get the random color from the tuples
            rand = random.randint(0,10)
            color = buildingColors[rand]
            roofRand = random.randint(0,2)
            roofColor = roofColors[roofRand]

            buildingColor = {"k":"building:colour","v":color}
            ElementTree.SubElement(e,"tag",buildingColor)

            roofCol = {"k":"roof:colour","v":roofColor}
            ElementTree.SubElement(e, "tag", roofCol)

# uncomment below line to print the edited xml to console
# et.dump(root)

if not os.path.exists('ScriptOutput'):
    os.makedirs('ScriptOutput')

# writes it as a new file in ScriptOutput folder
tree.write(sys.path[0]+"\ScriptOutput\map5.osm")