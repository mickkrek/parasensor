// trying to gain Petrakis' trust... maybe this is all variables going +1/-1 trust depending on your choices and possibly what you have equipped.
// i dont think you necessarily need a "perfect combination of right answers" bc thats annoying lol, it's mostly about the player's willingness to get into the conspiracy wavelength. the on/off ramps should be obvious enough...

->PETRAKIS_HUB
== PETRAKIS_HUB ==
->petrakis_Intro

== petrakis_Intro ==
PETRAKIS: Who's there?
+ 	Dr Petrakis? I need to talk to you.
	PETRAKIS: Who are you? What do you want?
-
// Petrakis opens the door just enough for us to see her face but that's it
PETRAKIS: Oh... You're from Metcom...
+	Technically, I'm just a contractor.
	PETRAKIS: Hmpf.
+	Yeah. Unfortunately.
	PETRAKIS: ...Hmmm.
+	Metcom is not that bad...
	PETRAKIS: Well, you have no idea. You have no idea what they do.
-
PETRAKIS: Why is Metcom sending lackeys to my address anyway? I know I didn't put my two weeks in, but since when do they care? 
// ask about metphone tampering
+	I was actually sent to fix the Metphone in the Eastern Exchange Building...
+	That's not what I'm here for, I'm here about the Metphone you broke.
-
PETRAKIS: Of course, I broke it. It was an imperative I couldn't ignore. It was in the numbers, in the data.
PETRAKIS: The chirping wires and the ultra-low frequency transmissions... the nurturing of coccoons in the fabric of the city... It's unacceptable.
PETRAKIS: Out of curiosity, did Choi rat me out? Always knew he'd fuck me over someday.
+   Yeah, he did. Immediately.
+   Took some effort, but he did.
+   Nah, I worked it out myself. 
-
PETRAKIS: Ah. Well.
PETRAKIS: Hmm. 
PETRAKIS: You seem smart enough. I'd be happy to tell you all about what Metcom's been doing under our feet for years.
->petrakis_MetphoneQuestions

== petrakis_MetphoneQuestions ==
PETRAKIS: Go on, ask me about that phone.
+   { break and powercore and quitting } Okay. I think I know what's going on here.
    ->petrakis_InProgress
+	Why did you break the phone? 
    ->break
+	What did you do with the power core? 
    ->powercore
+ 	Why did you run away from the Research team?
    ->quitting
+   Actually, I've got other things to do right now. (Leave) 
    PETRAKIS: Suit yourself. I'll be here whenever you decide to face the truth. ->DONE

= break
PETRAKIS: Think about it. What do you think they were doing with that thing? 
PETRAKIS: Spying on us, demanding regular contact... but not just that! There's something strange with the receiver. The voices... 
PETRAKIS: The other two, Choi, Jennings. They never went down to do the handshake. They always made me do it, because they reckoned the work was lesser, even though I'm more competent than both of them combined.
PETRAKIS: You know how men are.
PETRAKIS: It was always me. At first it was normal, just like every other handshake I've ever done in my twenty years here.
PETRAKIS: And then... The voices...
PETRAKIS: I can't unhear them. There is something evil and haunted in the wires.
+   Haha. The voices. Yeah. Isn't it just interference?
    PETRAKIS: No.
+   It couldn't just be interference...
    PETRAKIS: Exactly.
-
PETRAKIS: The Metphone is on a closed circuit. There can't be interference.
PETRAKIS: I had to break it. Sever the line between their world and ours. Maybe if you heard it you would understand better...
->petrakis_MetphoneQuestions

= powercore
PETRAKIS: The power core?
PETRAKIS: Have you seen inside the phone?
+   Yes. // if Marisol has opened the panel. or maybe she can also lie?
    PETRAKIS: Then you already know it's pointless to try and ram a power core in the guts of that thing.
+   No.
    PETRAKIS: Well, if you did, you'd know it's pointless to try and ram a power core in the guts of that thing.
-
->petrakis_MetphoneQuestions

= quitting
PETRAKIS: I used to love the job, you know? I really did. I thought it was the most important thing, and then it took everything from me...
PETRAKIS: Twenty years of service and for what!
ERGOMANIA: She burnt out.
->petrakis_MetphoneQuestions

// trying to convince her youre on her side... or NOT
== petrakis_InProgress ==
PARANOIA: She's definitely onto something. There's something seriously wrong with Metcom.
ERGOMANIA: She's just burnt out and needs something that justifies her quitting.
+   You're onto something. I always thought something was weird about Metcom.
    ->conspiracist
+   I don't think you're making sense.
    PETRAKIS: Yeah, I expected as much.
    ->rationalist


// breakthrough! Petrakis opens up the door a little more.
= conspiracist
PETRAKIS: Oh...?
PETRAKIS: You believe me?
+   Of course I do. I'm doing my own investigation into a Metcom-related mystery myself.
-
PETRAKIS: You're the first person to actually take me seriously about this.
PETRAKIS: What was your name again?
+   Marisol.
-
PETRAKIS: Marisol! You can call me Eleni.
PARANOIA: We're allies now. Co-counter-conspirators.
+   I do still need to fix that phone, or I won't be able to infiltrate Metcom anymore.
    PETRAKIS: Yes. About that, I think we can turn fixing the Metphone to our advantage.
    PETRAKIS: I need an organic sample. A transluscent egg-like sac that grows in human bodies...
    PETRAKIS: I found such an organism in a Hardware Store nearby. The sample I retrieved from there unfortunately did not survive some of my... experiments.
    PETRAKIS: Would you be able to fetch me one?
    ->DONE
+   Do you know anything about this egg?
    ->petrakis_Egg.conspiracist // if you have the egg

// the door closes
= rationalist
PETRAKIS: Please leave me alone.
PETRAKIS: I want nothing to do with Metcom anymore...
+   Wait! I still need to fix the Metphone.  // if you dont have the egg
    PETRAKIS: Good luck doing it without my help. 
    ->DONE
+   Wait! I need to know what's going on with this egg. //if you have the egg equipped/in inventory
    ->petrakis_Egg.rationalist

== petrakis_Egg ==

= rationalist
PETRAKIS: How did you... Where did...
PETRAKIS: Were you spying on my research as well?
PETRAKIS: Trying to take over my work and get credit for it?
PETRAKIS: Absolutely not.
PETRAKIS: I can't help you with that. Sorry.
ERGOMANIA: Yes she can. She's smarter than everyone else and knows what she's doing. Just needs convincing...
+   I'm not trying to get any credit for anything. I just want to fix this phone so I don't get fired. You're clearly smarter and better than me and I just need help.
-
PETRAKIS: And...?
+   Please help me.
-
PETRAKIS: Fine.
->hint

= conspiracist
PETRAKIS: The egg! There it is. Thank you.
->hint

= hint
PETRAKIS: Eggs like that one are somehow compatible with the Metphone.
PETRAKIS: Same frequency, same voltage. You could think of it as a miracle of life, but the truth is much simpler: Metcom has been "growing" these inside human hosts.
// if conspiracist: 
PETRAKIS: This is a great specimen. I think it'll survive the symbiotic relationship with...
PETRAKIS: My bug! Get this, Marisol. We're *actually* bugging Metcom HQ!
PETRAKIS: Here you go. Attach it to the egg once it's connected to the phone's guts.
// if not:
PETRAKIS: There you go. There's your hint.
PETRAKIS: Now go fix that phone, bootlicker.
->DONE