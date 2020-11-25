from pydub import AudioSegment
from pydub.playback import play
from multiprocessing import Process
import time
import random
import os
import socket
import http.server
import socketserver
import shutil

######################################## PARAMS #######################################################################
Numberofchordchanges = 100
cpb = 8
Fadein = 1000
Fadeout =1000
scale = "C#"
mode = "Aeolian"
bpm = 70
mpb = (60/bpm)*1000
totalsonglength = 10*60*10000
trail = 1000
HOST = 'localhost'
PORT = 60000 
looporg = "Cymatics - Caribbean Hihat Loop - 103 BPM" # without.wav
loopbpm = 103/2
#######################################################################################################################

Durationofeachchord = mpb * cpb # in milliseconds
silence = AudioSegment.silent(duration=Durationofeachchord)
chopduration = Durationofeachchord

def speed_change(sound, speed=1.0):
    # Manually override the frame_rate. This tells the computer how many
    # samples to play per second
    sound_with_altered_frame_rate = sound._spawn(sound.raw_data, overrides={
        "frame_rate": int(sound.frame_rate * speed)
    })

    # convert the sound with altered frame rate to a standard frame rate
    # so that regular playback programs will work right. They often only
    # know how to play audio at standard frame rate (like 44.1k)
    return sound_with_altered_frame_rate.set_frame_rate(sound.frame_rate)

def createkickpattern():
    kick_old = AudioSegment.from_file("Tracks/Cymatics-FireKick-D.wav")
    kick = kick_old[0:mpb]
    for k in range(0,15):
        kick = kick + kick_old[0:mpb]
        k=k+1
    kick[0:Durationofeachchord+2000].export("kickpattern.wav", format='wav')

def createlead(notearray):
    lead = AudioSegment.silent(duration=0)
    while True:
        l =0
        NoteL = notearray[random.randint(0, 12)]
        NoteLlength = random.randint(1, 6)
        silencer = random.randint(0,1)
        if(l == 0 and silencer == 0):
            lead = lead + AudioSegment.silent(duration=NoteLlength*mpb)
        else:
            audioL = AudioSegment.from_file("Tracks/"+str(NoteL)+"L.wav")
            lead = lead+audioL[0:NoteLlength*mpb]
            if(lead.duration_seconds>Durationofeachchord/1000):
                lead[0:Durationofeachchord].export("lead.wav", format='wav')
                return

def create():
    notearray = notesinscale(scale,mode)
    try:
        shutil.rmtree('export')
        os.mkdir("export")
    except:
        print('dir does not exist')
    for i in range(0,Numberofchordchanges):
        createkickpattern()
        createlead(notearray)
        Note1 = notearray[random.randint(0, 6)]
        Note2 = notearray[random.randint(0, 6)]
        Note3 = notearray[random.randint(0, 12)]
        Note4 = notearray[random.randint(0, 12)]
        Note5 = notearray[random.randint(0, 12)]
        Note6 = notearray[random.randint(6, 12)]
        audio1 = AudioSegment.from_file("Tracks/"+str(Note1)+".wav") - 5
        audio2 = AudioSegment.from_file("Tracks/"+str(Note2)+".wav") - 5
        audio3 = AudioSegment.from_file("Tracks/"+str(Note3)+".wav") - 5
        audio4 = AudioSegment.from_file("Tracks/"+str(Note4)+".wav") - 5
        audio5 = AudioSegment.from_file("Tracks/"+str(Note5)+".wav") - 5
        audio6 = AudioSegment.from_file("Tracks/"+str(Note6)+".wav") - 5
        kickpattern = AudioSegment.from_file("kickpattern.wav") - 10
        lead = AudioSegment.from_file("lead.wav") + 4
        FX1 = AudioSegment.from_file("Tracks/Cymatics - White Noise Upsweep - 130 BPM.wav") - 10
        FX2 = AudioSegment.from_file("Tracks/Cymatics - Temple Impact.wav") - 10
        loop = AudioSegment.from_file("Tracks/"+looporg+".wav") - 10
        loop = speed_change(loop,(bpm/loopbpm))
        if(i==0):
            try:
                os.remove("mixed.wav")
                os.remove("kickpattern.wav")
                os.remove("lead.wav")
            except:
                print("file does not exist")
            mixed_old = audio1.overlay(audio2).overlay(audio3).overlay(audio4).overlay(audio5).overlay(audio6)
            mixed_old[:Durationofeachchord].fade_out(Fadeout).export("mixed.wav", format='wav')
            
        else:   
            mixed_new = audio1.overlay(audio2).overlay(audio3).overlay(audio4).overlay(audio5).overlay(audio6).fade_out(Fadeout)
            mixed_old = mixed_old + silence
            
            mixedtest = mixed_old.overlay(mixed_new,position= len(mixed_old) - Durationofeachchord - trail)
            if((i>1 and i<20) or(i>22 and i<65)):
                mixedtest = mixedtest.overlay(kickpattern,position= len(mixed_old) - Durationofeachchord - trail).overlay(lead,position= len(mixed_old) - Durationofeachchord - trail)
                mixedtest = mixedtest.overlay(FX2,position= len(mixed_old) - Durationofeachchord - trail)
                mixedtest = mixedtest.overlay(loop*2,position= len(mixed_old) - Durationofeachchord - trail)
            if(i%10==0):
                mixedtest = mixedtest.overlay(FX1,position= len(mixed_old) - Durationofeachchord - trail)
            mixedtest.export("mixed.wav", format='wav') 
            mixedtest[i*Durationofeachchord:(i+1)*Durationofeachchord].export("export/mixedstream"+str(i)+".ogg", format='ogg', codec="libvorbis") 
            #mixedtest.export("mixedcompatible.ogg", format='ogg', codec="libvorbis") 
            mixed_old = mixedtest
            if(i>25):
                time.sleep(chopduration/1000)
            else:
                time.sleep((Durationofeachchord-2000)/1000)

def stopwatch(sec):
    while sec:
        minn, secc = divmod(sec, 60)
        timeformat = '{:02d}:{:02d}'.format(minn, secc)
        print(timeformat, end='\r')
        time.sleep(1)
        sec -= 1
    print('PLAYING NOW!\n')

def produce():
    print("making moooosic - WAIT!!")
    stopwatch(15)
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
    time.sleep(3)
    for m in range(1,Numberofchordchanges):
        #if not(os.path.exists("export/mixedstream"+str(m))):
        #    time.sleep(1)
        os.system('ffmpeg -re -i export/mixedstream'+str(m)+'.wav -ab 128k -f ogg udp://127.0.0.1:' + str(PORT))
        if(m%10 == 0):
            for d in range(1,9):
                #os.remove('export/mixedstream'+str(m - d)+'.wav')
                print("")

def startserver():
    Handler = http.server.SimpleHTTPRequestHandler
    httpd = socketserver.TCPServer(("", PORT), Handler)
    print("serving at port", PORT)
    httpd.serve_forever()

def playserver():
    os.system('ffplay udp://127.0.0.1:' + str(PORT))

if __name__ == '__main__':
    p1 = Process(target=create)
    p1.start()
    #p2 = Process(target=letsstream)
    p3 = Process(target=startserver)
    #p2.start()
    p3.start()
    p1.join()
    #p2.join()
    p3.join()
