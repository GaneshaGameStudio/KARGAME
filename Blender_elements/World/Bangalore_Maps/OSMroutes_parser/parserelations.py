#not the best optimised but it gets us there
import lxml.etree
import math
import os

routetype = "platform"

if not os.path.exists('bus_routes'):
    os.makedirs('bus_routes')
if not os.path.exists('platforms'):
    os.makedirs('platforms')

if(routetype == ""):
    routedirectory = "bus_routes/"
elif(routetype == "platform"):
    routedirectory = "platforms/"

earthradius = 6371
org_coord=[13.0151,77.5548,13.0157,77.5555] # window

def haversine(x1,y1,x2,y2):
    havlat = math.sin(math.radians((x1-x2)/2))**2
    havlong = math.sin(math.radians((y1-y2)/2))**2 * math.cos(math.radians(x1))*math.cos(math.radians(x2))
    
    c = 2 * math.atan2((havlat + havlong)**0.5,(1 - havlat + havlong)**0.5)
    return 1000*earthradius*c

totalwide = haversine(org_coord[0],org_coord[1],org_coord[0],org_coord[3]) 
totalheight = haversine(org_coord[0],org_coord[1],org_coord[2],org_coord[1])
org_dist=[-totalwide/2.,-totalheight/2.,totalwide/2.,totalheight/2.] #viewport
sx = (org_dist[2]-org_dist[0])/(org_coord[2]-org_coord[0])
sy = (org_dist[3]-org_dist[1])/(org_coord[3]-org_coord[1])

def convertcoords(lat,lon):
    myfile.write(str(org_dist[0] + (lat - org_coord[0])*sx) + "," +str(org_dist[1] + (lon - org_coord[1])*sy) + "\n")
    return 

def getcoords(node,typ):
    for ndcoord in tree.findall('//node[@id="'+str(node)+'"]'):
        lat = ndcoord.get('lat')
        lon = ndcoord.get('lon')
        convertcoords(float(lat),float(lon))
        #print(round(latd,6),round(lond,6),typ)

def getwayid():
    for member in tag.getparent():
        if(member.get('type') == "node"):
            noderef = member.get('ref')
            if(member.get('role')==routetype):
                getcoords(noderef,member.get('role'))
        else:
            wayref = member.get('ref')
            for wyref in tree.findall('//way[@id="'+str(wayref)+'"]'):
                for noderef in wyref:
                    if(member.get('role')==routetype):
                        getcoords(noderef.get('ref'),member.get('role'))

tree = lxml.etree.parse('map-3.xml')
for tag in tree.findall('//relation//tag[@k="operator"][@v="BMTC"]'):
    routenumbercheck = tag.getprevious().getprevious().get('k')
    if(routenumbercheck == "name"):
        print(tag.getprevious().getprevious().get('v'))
        if not os.path.exists(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv'):
            with open(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()
        else:
            os.remove(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv')
            with open(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()

    else:
        print(tag.getprevious().getprevious().getprevious().get('v'))
        if not os.path.exists(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv'):
            with open(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()
        else:
            os.remove(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv')
            with open(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()



