== choi_InProgress ==
CHOI: ...if this is like this then how is it like that...
// Choi looks up
CHOI: Oh. 
CHOI: Marisol. Anything you need?
-> choi_questionsMenu

== choi_questionsMenu ==
// questions hub that updates as you do things. variables tbd depending on how we wanna implement this stuff.
+	{ metphone_Inspect } I've looked at the phone, looks like someone tampered with it. ->choi_metphoneInspected 
+	{ oldphone_Inspect } By the way, I found a very old phone in the basement. Know anything about it? ->choi_oldphoneInspected
+	{ petradesk_Inspect && not choi_petradeskConfess } (Point at messy desk) { jennings_petradeskInspected: So, where's the third member of your team? | Whose desk is that? } ->choi_petradeskInspected
+ 	Nothing right now, cheers. (Leave) ->DONE

= choi_metphoneInspected
// he acts guilty about something... but he tells you about the hardware store. paranoia tells you to look around more if you havent found petrakis' desk.
CHOI: Someone...? 
CHOI: Well, I haven't been down in the basement in months so it wouldn't be me, in case you're wondering.
+	I'm not implying anything, but okay.
+	Okay.
-
CHOI. Mm-hmm.
CHOI: How... *tampered* is it?
+	The core's missing, someone deliberately removed it.
// should there be a lying option here... maybe
-
CHOI: So it's not *completely* destroyed...
CHOI: How long will it take for you to replace it?
+	I'd need to source a replacement first. Would there be any proprietary parts lying around here?
CHOI: There's a Hardware Store nearby that stocks... seemingly everything. That's where I got the generator.
CHOI: They may have some Metcom stuff there too.
CHOI: Uh... Anyway. Maybe you should check that out instead of looking around here.
PARANOIA: He's hiding something... We sense guilt, but it's not about the phone.
{   
    - 	petradesk_Inspect && jennings_petradeskInspected:
		<> Maybe he's responsible for Petrakis running away...
    - 	petradesk_Inspect:
		<> Could it have something to do with that messy desk?
    - 	else:
		<> He *really* didn't want us to look around here. There must be something closeby he doesn't want us to see. 
}
{ petradesk_Inspect && not jennings_petradeskInspected: PARANOIA: Maybe Jennings knows something...? }
CHOI: ...Anything else?
->choi_questionsMenu

= choi_oldphoneInspected
// he doesnt know anything about this but will point you to jennings
CHOI: An old phone? How old are we talking about?
+	Says "MTEC - 1887" on it.
+	VERY old. Made of wood and stuff. // maybe this is an option if you didnt see the date? i dunno.
-
CHOI: Hm. I... wouldn't know anything about something that old.
CHOI: Jennings might, though. He's... obsessed with the past.
CHOI: ...old bitch... // SHOULD HE SAY OLD BITCH...?
CHOI: Anything else?
->choi_questionsMenu


= choi_petradeskInspected
// if you havent diagnosed the phone, he'll be evasive and tell you about someone who used to work there. if you did inspect the phone, you can really press him about petrakis and he'll confess and tell you the real story with the phone.
{ 
	- jennings_petradeskInspected: ->choi_petradeskConfess
	- else: ->choi_petradeskDeflect
}

= choi_petradeskDeflect
CHOI: Uh, um, well.
CHOI: That's not anyone's desk. It was like that even before we got there.
+   Multiple documents on it explicitly refer to "Dr. Jennings' research team"...
-
CHOI: Oh, well, of course. Um, Jennings must have put them on there.
CHOI: How careless, to put our documents on some random desk... haha. 
CHOI: Bosses, am I right?
PARANOIA: Oh he's lying. He's obviously lying to us.
ERGOMANIA: Yeah, he's godawful at it. Poor bastard. Wonder what Jennings would have to say about the desk?
+	Haha... yeah.
+	\(Say nothing)
-
CHOI: Haha. Is there anything else? 
->choi_questionsMenu

= choi_petradeskConfess
CHOI: How... uh... um... there's no third member of the team. No idea what you're talking about.
+	{ not choi_petradeskDeflect } Multiple documents on the desk explicitly refer to "Dr. Jennings' research team"...
	CHOI: Oh, well, of course. Um, Jennings must have put them on there.
	CHOI: How careless, to put our documents on some random desk... haha. 
	PARANOIA: Oh he's lying. He's obviously lying to us.
	ERGOMANIA: Yeah, he's godawful at it.
	CHOI: Bosses, am I right?
+	-> // fallback empty choice
-
PARANOIA: C'mon man...
+   Look, Jennings told me about her. Petrakis, right?
-
CHOI: ...
CHOI: Fuck. Alright. Yeah, she was the other assistant. 
CHOI: She's always been an excentric, but recently she... well, she lost her mind.
CHOI: Started talking about how the wires are listening in, something about "tendrils" and voices in DTMF signals... evil coming in from the MetPhone in the basement... crazy stuff. 
CHOI: Three days ago, she said she "took care of the source" and that she's "going to work off-site". Then the lights went out. 
CHOI: We haven't seen her since.
// asks if marisol can check on her (guilt), explicitly tells you where she is staying.
CHOI: (Sigh)
PARANOIA: There's the guilt again...
CHOI: This is going to sound weird, but...
CHOI: Can you check in on her? Although I only know she's staying at a hotel, but I don't know which.
+	Yeah. Acheron Hotel. It's not too far from here. // if pen is in inventory?
+	I can see if she left any clues on her desk. // if not?
-
CHOI: Thanks.
+   Wait, why did you lie and pretend she didn't exist?
	CHOI: I didn't think it'd be important to mention.
	PARANOIA: We need to push him further. Make him squirm.
	ERGOMANIA: Does it matter? We've got the information we need...
	++	Come on, I know there's more to it. I'm sure Jennings would love to know you made it harder for the phone to be repaired...
		CHOI: (Sigh)
	    CHOI: Eleni and I started working at Metcom around the same time. We immediately got along.
	    CHOI: She was good. Really good at her job. So good that her husband left her.
	    ERGOMANIA: She was married to the work, huh? Sounds familiar.
	    CHOI: After the divorce, she got really into... what do you call it? New Age stuff? It was surprising considering how methodologically scientific she is. She was always going on about transmissions and the evil eye. It got worse when we were transferred here a few months ago.
	    CHOI: She was still good at her job, though. And after years of service she started getting recognition and awards from the bigger Research teams... they wanted to move her to the big shot team at HQ.
	    CHOI: I couldn't stand it. Her moving on up and me being stuck with Jennings. You know they sent him here because they couldn't fire him? Being here is worse than being fired.
	    CHOI: I started fabricating things. Making puzzles for her to solve, encouraging the conspiracy stuff. I'd make a different set of data for her to look at, I researched numerology just for that.
	    CHOI: She got deeper and deeper into it, way further than I thought she would.
	    CHOI: I thought I'd do it just enough for HQ Research to decide against moving her... but the damage was done. I didn't need to do anything anymore, the spiral sustained itself, you know.
	    CHOI: And then a few days ago she crossed some threshold, past the event horizon. And here we are now.
	    CHOI: When you brought her up... I panicked. I didn't think you'd notice.
	    CHOI: It's probably my fault she's gone.
	    CHOI: That's... that's why I need you to check on her.
	    +++ Ah... shit. I'm sorry for pushing you.
	        CHOI: It's... it's okay. I shouldn't have lied.
	    +++ Well, that's fucking evil, man.
	    	CHOI: I know. I'm sorry. I'm so sorry.
	    +++ Okay. Sure.
	++	Fine, whatever. None of my business anyway.
	    CHOI: Uh.. Thanks...
+	\(Letting the whole lying thing slide) Cool. 
-
CHOI: Is... is there anything else you need?
->choi_questionsMenu