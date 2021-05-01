using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Translater : MonoBehaviour
{   
    private string[] Latnaddvowel;
    private string[] Vattuletter;
    private string[] Latnletter;
    private string[] Kanletter;
    public TextMeshProUGUI Translated;

    private string translatedinput;
    // Start is called before the first frame update
    void Start()
    {
        Latnletter = new string[]{"C","D","E","F","G","H","I","J","K","L","M","N","O",
                                "P","S","U","W","kõ",
                                "Z","b","e","gÀhi","Yõ",
                                "l","o","q","qs","u",
                                "v","x","z","zs","£",
                                "¥","¥s","¨","¨s","ªÀi",
                                "AiÀi","g","¯","ª","±","µ","¸","º","¼","A","B","."};
        Kanletter = new string[]{"a","A","i","I","u","U","R","e","E","ai","o","O","au",
                                "k","K","g","G","~j",
                                "c","C","j","J","~g",
                                "t","T","d","D","N",
                                "th","Th","dh","Dh","n",
                                "p","P","b","B","m",
                                "y","r","l","v","S","Sh","s","h","L","M","H","."};

        Latnaddvowel = new string[]{"À","Á","","Ã","ÀÄ","ÀÆ","ÀÈ","É","ÉÃ","ÉÊ","ÉÆ","ÉÆÃ","PË"};
        Vattuletter = new string[]{"","","","","","","","","","","","","",
                                "Ì","Í","Î","Ï","Õ",
                                "Ñ","Ò","Ó","ïÔ","Ð",
                                "Ö","×","Ø","Ù","Ú",
                                "Û","Ü","Ý","Þ","ß",
                                "à","á","¨â","¨sïã","ä",
                                "å","æ","è","é","ê","ë","ì","í","î","","",""};
    }

    public void Readstring(string s){
        translatedinput=null;
        for (int counter = 0; counter < s.Length; counter++)
        {   
            
            int match = 0;
            match = Array.IndexOf(Kanletter, s[counter].ToString());
            // independent vowel
            if(match < 12){
                            //single character
                            if(s.Length == 1 || s[counter]!='a'){
                                translatedinput+=Latnletter[match];
                            }
                            //independent vowel special cases
                            else {
                                if(counter<s.Length-1){
                                    if(s[counter+1] =='i' || s[counter+1] =='u'){
                                        match = Array.IndexOf(Kanletter, s[counter] + s[counter+1].ToString());
                                        translatedinput+=Latnletter[match];
                                        counter = counter+1;
                                    }
                                    else{
                                        translatedinput+=Latnletter[match];
                                    }
                                }
                                else{
                                    translatedinput+=Latnletter[match];
                                }
                            }
                        }
                    
            // kannada any letter
            else{
                // check succeeding letter for a vowel
                if(counter!=s.Length-1){
                    int nextletter = Array.IndexOf(Kanletter, s[counter+1].ToString());
                    
                    //is it followed by h, then combine it
                    if(nextletter==45){
                        match = Array.IndexOf(Kanletter, s[counter] + 'h'.ToString());
                        counter=counter+1;
                        nextletter = Array.IndexOf(Kanletter, s[counter+1].ToString());
                    }
                    
                    if(nextletter<12){
                        //check for ai and au
                        if(nextletter == 0){
                            
                            if(counter<s.Length-2){
                                if(s[counter+2] =='i' || s[counter+2] =='u'){
                                    nextletter = Array.IndexOf(Kanletter, s[counter+1] + s[counter+2].ToString());
                                    translatedinput+=Latnletter[match]+ Latnaddvowel[nextletter];
                                    counter=counter+1;
                                }
                                else{
                                    translatedinput+=Latnletter[match] + Latnaddvowel[nextletter];
                                }
                            }
                            else{
                                translatedinput+=Latnletter[match] + Latnaddvowel[nextletter];
                                print(translatedinput);
                                }
                            
                        }
                        //add special case for i and I
                        else if(nextletter == 2 || nextletter == 3){
                            char special = char.Parse(Latnletter[match]);
                            special++;
                            //special ti case
                            if(special=='m'){
                                special++;
                            }                            
                            translatedinput+=special + Latnaddvowel[nextletter];
                        }
                        else{
                            translatedinput+=Latnletter[match] + Latnaddvowel[nextletter];
                        }
                    
                    counter = counter+1;
                    }
                        else{
                            //standalone
                            //fullstop
                            if(match==Kanletter.Length){
                                
                                translatedinput+=Latnletter[match];
                            }
                            else if(counter==s.Length-2){
                                
                                translatedinput+=Latnletter[match] + "ï";
                                match = Array.IndexOf(Kanletter, s[counter+1].ToString());
                                translatedinput+=Vattuletter[match];
                                counter=counter+1;
                            }
                            else{
                                
                                nextletter = Array.IndexOf(Kanletter, s[counter+2].ToString());
                                if(nextletter == 2 || nextletter == 3){
                                    char special = char.Parse(Latnletter[match]);
                                    special++;
                                    translatedinput+=special + Latnaddvowel[nextletter];
                                    }
                                else{
                                    translatedinput+=Latnletter[match] + Latnaddvowel[nextletter];
                                    }
                                match = Array.IndexOf(Kanletter, s[counter+1].ToString());
                                //check vattakshara
                                translatedinput+=Vattuletter[match];
                                counter=counter+2;
                                }
                            }
                        
                }
                else{
                    translatedinput+=Latnletter[match] + "ï";
                    
                    }
                }
            }
        Translated.SetText((translatedinput).ToString());

    }
    // Update is called once per frame
    void Update()
    {   
        //check vowel
        
    }
}
