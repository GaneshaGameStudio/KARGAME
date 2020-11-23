from pydub import AudioSegment
from pydub.playback import play
from multiprocessing import Process
import time
import random
import os
import socket

Numberofchordchanges = 100
Durationofeachchord = 12000 # in milliseconds
Fadein = 1000
Fadeout =1000
scale = "D"
mode = "Lydian"
bpm = 60
mpb = (60/bpm)*1000
totalsonglength = 10*60*10000
def createkickpattern():
    kick_old = AudioSegment.from_file("Tracks/Cymatics-FireKick-D.wav")
    kick = kick_old[0:mpb]
    for k in range(0,15):
        kick = kick + kick_old[0:mpb]
        k=k+1
    kick[0:Durationofeachchord].export("kickpattern.wav", format='wav')

def create():
    notearray = notesinscale(scale,mode)
    for i in range(0,Numberofchordchanges):
        createkickpattern()
        Note1 = notearray[random.randint(0, 12)]
        Note2 = notearray[random.randint(0, 12)]
        Note3 = notearray[random.randint(0, 12)]
        Note4 = notearray[random.randint(0, 12)]
        Note5 = notearray[random.randint(0, 12)]
        Note6 = notearray[random.randint(0, 12)]
        audio1 = AudioSegment.from_file("Tracks/"+str(Note1)+".wav") -5
        audio2 = AudioSegment.from_file("Tracks/"+str(Note2)+".wav")-5
        audio3 = AudioSegment.from_file("Tracks/"+str(Note3)+".wav")-5
        audio4 = AudioSegment.from_file("Tracks/"+str(Note4)+".wav")-5
        audio5 = AudioSegment.from_file("Tracks/"+str(Note5)+".wav")-5
        audio6 = AudioSegment.from_file("Tracks/"+str(Note6)+".wav")-5
        kickpattern = AudioSegment.from_file("kickpattern.wav") - 7

        if(i==0):
            try:
                os.remove("mixed.wav")
                os.remove("kickpattern.wav")
            except:
                print("file does not exist")
            mixed_old = audio1[:Durationofeachchord].overlay(audio2).overlay(audio3).overlay(audio4).overlay(audio5).overlay(audio6)
            mixed_old.fade_out(Fadeout).export("mixed.wav", format='wav')
        else:   
            mixed_new = audio1.overlay(kickpattern).overlay(audio2).overlay(audio3).overlay(audio4).overlay(audio5).overlay(audio6)
            
            mixed  = mixed_old + mixed_new[0:Durationofeachchord].fade_out(Fadeout)
            mixed_new[0:Durationofeachchord].fade_out(Fadeout).export("mixednew.wav", format='wav') 
            mixed.fade_out(Fadeout).export("mixed.wav", format='wav') 
            mixed_old = mixed
            
            if(i>25):
                time.sleep(10)
            else:
                time.sleep(0)

def produce():
    time.sleep(5)
    j=0
    renderaudio = AudioSegment.from_file("mixed.wav",format="wav") 
    while(j>=0):
        if(j!=0):
            start = len(renderaudio)
            renderaudio = AudioSegment.from_file("mixed.wav",format="wav")     
        else:
            start = 0
        play(renderaudio[start:])    
        j=j+1                       

def rootnote(scale):
    if(scale=="C"):
        return 0
    elif(scale=="C#"):
        return 1
    elif(scale=="D"):
        return 2
    elif(scale=="D#"):
        return 3
    elif(scale=="E"):
        return 4
    elif(scale=="F"):
        return 5
    elif(scale=="F#"):
        return 6
    elif(scale=="G"):
        return 7
    elif(scale=="G#"):
        return 8
    elif(scale=="A"):
        return 9
    elif(scale=="A#"):
        return 10
    elif(scale=="B"):
        return 12

def notesinscale(note,scale):
    RN = rootnote(note)    
    
    if(scale=="Ionian"):
        scalenotes = [0,2,4,6,8,9,12]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull
    
    if(scale=="Dorian"):
        scalenotes = [0,2,3,5,7,9,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

    if(scale=="Phrygian"):
        scalenotes = [0,1,2,5,7,8,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

    if(scale=="Lydian"):
        scalenotes = [0,2,3,6,7,9,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

    if(scale=="Mixolydian"):
        scalenotes = [0,2,4,5,7,9,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

    if(scale=="Aeolian"):
        scalenotes = [0,2,3,5,7,8,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

    if(scale=="Locrian"):
        scalenotes = [0,1,3,5,6,8,10]
        new_list = [x+RN for x in scalenotes]
        newlistfull = new_list + [x*2 for x in new_list]
        return newlistfull

def letsstream():
    time.sleep(2)
    s=socket.socket(socket.AF_INET,socket.SOCK_STREAM); s.connect((HOST,PORT))
    with open('mixed.wav', 'rb') as f:
        for l in f: s.sendall(l)
        s.close()

if __name__ == '__main__':
    p1 = Process(target=create)
    p1.start()
    p2 = Process(target=produce)
    p2.start()
    p1.join()
    p2.join()
