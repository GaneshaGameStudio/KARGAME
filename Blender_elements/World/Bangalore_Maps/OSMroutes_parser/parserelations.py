#not the best optimised but it gets us there
import lxml.etree
import math
import os

routetype = ""
mapfile = 'map4.xml'
if not os.path.exists('bus_routes'):
    os.makedirs('bus_routes')
if not os.path.exists('platforms'):
    os.makedirs('platforms')

if(routetype == ""):
    routedirectory = "bus_routes/"
elif(routetype == "platform"):
    routedirectory = "platforms/"

earthradius = 6371
org_coord=[12.9886999,77.5167999,13.0481997,77.5847015] # window
der_coord=[12.9743000,77.4893000,13.0658000,77.6066000]

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
    #print(lat,lon)
    if(lat>org_coord[0]):
        if(lon>org_coord[1]):
            myfile.write(str(org_dist[0] + haversine(lat,lon,lat,org_coord[1])) + "," +str(org_dist[1] + haversine(lat,lon,org_coord[0],lon)) + "\n")
    if(lat<org_coord[0]):
        if(lon>org_coord[1]):
            myfile.write(str(org_dist[0] + haversine(lat,lon,lat,org_coord[1])) + "," +str(org_dist[1] - haversine(lat,lon,org_coord[0],lon)) + "\n")
    if(lat>org_coord[0]):
        if(lon<org_coord[1]):
            myfile.write(str(org_dist[0] - haversine(lat,lon,lat,org_coord[1])) + "," +str(org_dist[1] + haversine(lat,lon,org_coord[0],lon)) + "\n")
    if(lat<org_coord[0]):
        if(lon<org_coord[1]):
            myfile.write(str(org_dist[0] - haversine(lat,lon,lat,org_coord[1])) + "," +str(org_dist[1] - haversine(lat,lon,org_coord[0],lon)) + "\n")
    return 

def getcoords(node,typ):
    for ndcoord in tree.findall('//node[@id="'+str(node)+'"]'):
        lat = ndcoord.get('lat')
        lon = ndcoord.get('lon')
        convertcoords(float(lat),float(lon))

def getwayid():
    for member in tag.getparent():
        if(member.get('type') == "node"):
            noderef = member.get('ref')
            if(member.get('role')==routetype):
                getcoords(noderef,member.get('role'))
        else:
            wayref = member.get('ref')
            #print(wayref)
            for wyref in tree.findall('//way[@id="'+str(wayref)+'"]'):
                for noderef in wyref:
                    if(member.get('role')==routetype):
                        getcoords(noderef.get('ref'),member.get('role'))

tree = lxml.etree.parse(mapfile)
for tag in tree.findall('//relation//tag[@k="operator"][@v="BMTC"]'):
    routenumbercheck = tag.getprevious().getprevious().get('k')
    if(routenumbercheck == "name"): 
        print(tag.getprevious().getprevious().get('v'))
        if not os.path.exists(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv'):
            with open(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()    
        else:
            continue
            os.remove(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv')
            with open(routedirectory+str(tag.getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()
    else:
        print(tag.getprevious().getprevious().getprevious().get('v'))
        if not os.path.exists(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv'):
            with open(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()    
        else:
            continue
            os.remove(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv')
            with open(routedirectory+str(tag.getprevious().getprevious().getprevious().get('v'))+'.csv', 'a') as myfile: 
                getwayid()
                
    myfile.close()



