EXTERNAL SetCharacterState(characterCodeName, state)
->choi_Intro
== choi_Intro ==
// checking if marisol spoke to jennings first...
{
    - jennings_Intro: -> jennings_IntroFirst
    - not jennings_Intro: -> choi_IntroFirst
}

	= choi_IntroFirst
	// Choi is absorbed in his work, muttering to himself
	CHOI: ...well, these numbers don't even make sense... did she fuck them up somehow?
	ERGOMANIA: Ah, this is the guy that actually does the work around here. He'll appreciate being talked to first.
	+ 	Hey, excuse me. ->choi_situationBrief
	+	Hm... I'll leave you to your work for now. (Leave) ->DONE // exit convo here
	->DONE

	= jennings_IntroFirst
	// Choi is absorbed in his work, muttering to himself
	ERGOMANIA: This is the guy that actually does the work around here. Should have talked to him first.
	CHOI: ...bastard... treating me like I'm his lackey when I'm...
	+ 	Okay. Hello. Hey. ->choi_situationBrief
	->DONE

== choi_situationBrief ==
// choi explains what's goin on (omitting anything about petrakis), tells marisol where the phone is so she can take a look.

// Choi looks surprised to be caught muttering
	CHOI: Ah, oh, you're uh...
	// He changes his stance? Trying to be more "open"?
	CHOI: So, you're here for the MetPhone situation?
	CHOI: You were quick. Usually takes them a week to send a guy down here.
	+ 	I'm your guy.
	+ 	Not a guy but... I'm here.
	-
	CHOI: Yes. Well. 
	// i think the following part flows better as a series of linear questions than a question menu
	+	What's up with this phone anyway?
	-
	CHOI: Err, the phone... broke down. 
	PARANOIA: Why did he hesitate before saying "broke down"?
	+ 	What happened to it?
	-
	CHOI: I don't know, isn't it your job to figure that out? 
	CHOI: It's giving us enough trouble as it is! Our power went out at the same time as the phone died, and we can't just leave this monitoring station. It's important stuff.
	CHOI: Good thing we found this generator to keep our gear running... still lost about six hours of work, but it's better than a complete shut down.
	// i want a neurosis chiming in here
	+	Alright. Where's this phone?
	-
	CHOI: The MetPhone's in the lowest basement level. Obviously. How do you not know that?
	+	Well...
	-
	CHOI: Don't answer that. 
	CHOI: Just take the stairs down. They're on the left of here.
	CHOI: Oh, by the way, what's your name?
	+	I'm Marisol.
	+	Marisol Mendez.
	-
	~ SetCharacterState("CHOI", 1) // CHOI
	CHOI: Marisol. I'm Choi, Research Assistant.
	~ SetCharacterState("JENNINGS", 1) // JENNINGS
	CHOI: And... that's Jennings. 
	CHOI: I mean...
	~ SetCharacterState("JENNINGS", 2) // DR. JENNINGS
	CHOI: *Doctor* Jennings. He's the one in charge of the team. 
	CHOI: Technically. 
	CHOI: Let me know if there's anything you need help with, I guess.
	->DONE

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
+	{ petradesk_Inspect and not choi_petradeskConfess } (Point at messy desk) { jennings_petradeskInspected: So, where's the third member of your team? | Whose desk is that? } ->choi_petradeskInspected
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
+	The access panel to the core's got a bunch of scratches on it. My guess is someone removed the core from it.
// should there be a lying option here... maybe
-
CHOI: So it's not *completely* destroyed...
CHOI: What do you mean your "guess", are you sure that's the cause?
+	It's the only part of it that is visibly damaged...
+	Are you a mechanic? It's pretty obvious the core compartment's the cause.
-
CHOI: Right, okay. How long will it take for you to replace it?
+	I'd need to open it up first. Would there be any proprietary screwdriver bits lying around here?
-
CHOI: Doubt it. They removed anything useful before we moved in. Including auxiliary power.
CHOI: There *is* a Hardware Store nearby that stocks... seemingly everything. That's where I got the generator.
CHOI: They may have some Metcom-compatible stuff there too.
CHOI: Uh... Anyway. Maybe you should check that out instead of looking around here.
PARANOIA: He's hiding something... We sense guilt, but it's not about the phone.
{   
    - 	petradesk_Inspect and jennings_petradeskInspected:
		<> Maybe he's responsible for Petrakis running away...
    - 	petradesk_Inspect:
		<> Could it have something to do with that messy desk?
    - 	else:
		<> He *really* didn't want us to look around here. There must be something closeby he doesn't want us to see. 
}
{ petradesk_Inspect and not jennings_petradeskInspected: PARANOIA: Maybe Jennings knows something...? }
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

== choi_Completed ==
CHOI: ...a spike in activity...?
+	Hey. Just fixed the phone. Don't know about the lights, though.
-
CHOI: Great, thanks Marisol.
CHOI: The lights... I'm sure they'll send someone else to do it, somehow.
->DONE

== jennings_Intro ==

ERGOMANIA: Wow, that's some advanced single-finger typing. This guy is hard at work.
+ 	\(Clear throat) Excuse me.
+	Uh... I'll leave you alone for now. (Leave) ->DONE // exit convo here
-

// this checks if you spoke to choi first
{
    - not choi_Intro: ->jennings_IntroFirst
    - choi_Intro: ->choi_IntroFirst
}

	= jennings_IntroFirst
	JENNINGS: ...
	+ 	I'm here to fix a phone...?
	+ 	Hello...?
	-
	JENNINGS: ...
	PARANOIA: He's ignoring us. He knows we're not *actually* up to the task. 
	ERGOMANIA: No way. He's just really, really focusing on work. Which at the moment seems to be entirely about typing words with two out of the ten fingers he has. I wouldn't want this guy as my boss.
	+ 	Fine. I'll just talk to your colleague.
	+ 	\(Stare intently) ...
		// Turning to Marisol for the first time
		JENNINGS: Quit staring at me like that!
		// Turning back towards the computer and muttering
		JENNINGS: Disrupting my focus like that. Unbelievable.
		// Turning to Marisol again
		~ SetCharacterState("CHOI", 1)
		JENNINGS: Just... talk to Choi.
		JENNINGS: Choi?
		CHOI: (Sigh) What?
		JENNINGS: That guy. Talk to him. Alright?
	-
	->DONE

	= choi_IntroFirst
	// Staring at the computer
	JENNINGS: Leave me alone, kid. Busy.
	JENNINGS: Don't ya have a phone to check on?
	->DONE

== jennings_InProgress ==
// leading into the jennings question hub
JENNINGS: What now? Keep it short. I'm very busy.
-> jennings_questionsMenu

== jennings_questionsMenu ==
// questions hub that updates as you do things. variables tbd depending on how we wanna implement this stuff.
+	{ metphone_Inspect } I've checked on the phone. Someone tampered with it. ->jennings_metphoneInspected
+   { oldphone_Inspect } I found an antique phone in the basement. { choi_questionsMenu.choi_oldphoneInspected: Choi told me you could tell me about it. | Would you know anything about it? } ->jennings_oldphoneInspected
+	{ petradesk_Inspect } (Point at messy desk) Do you know whose desk that is? ->jennings_petradeskInspected
+ 	I'm good for now. (Leave) ->DONE

== jennings_metphoneInspected ==
// jennings isnt much help here, but he does mention the generator and points you to choi, who is the one who grabbed it from the hardware store.
JENNINGS: Tampered? Someone broke it?
JENNINGS: Christ... which one of them...
JENNINGS: Ah, must be Choi, that useless prick. Always making things hard for me.
JENNINGS: I should report him and have him fired once and for all... he's got two strikes against him already...
JENNINGS: But then the work would slow down and... I lose the leverage I have over him... Ha! No, I can't do that yet.
ERGOMANIA: This man is a parasite. He's got no respect for any of the work that's being done around here.
+	Right. You wouldn't know anything about getting the missing part for it?
-
JENNINGS: Absolutely not. That's Choi work, not Jennings work.
JENNINGS: He sourced the generator. That's been... helpful, although we did lose six hours of work because of how slow he was in bringing that generator here.
JENNINGS: What else? I need to get back to this.
->jennings_questionsMenu

== jennings_oldphoneInspected ==
// jennings special interest <3
JENNINGS: Ah-a! An antique? What year?
+	1887?
+	Uhhh, late nineteenth century.
-
JENNINGS: A MTEC Class B unit then, most likely! Those are beautiful machines.
JENNINGS: MTEC stands for Metropoliton Telephone Exchange Company. If you ever wondered where the name "Metcom" came from...
JENNINGS: The yanks would never admit to it, but the first sound-powered telephones were actually invented here, in this City, a good sixty years before they ever even thought of one. A miracle of good old Australian engineering!
ERGOMANIA: He sounds like a different person now that we're talking about something he likes. I'd encourage him to continue, there's probably some valuable information in here somewhere.
+	Sound-powered...?
-
JENNINGS: I'm glad a young girl like you is interested in such machinery. Let me explain. Sound-powered telephones like the one you found are entirely independent from any telephone exchange network. You don't need any external power. As long as the circuit is live, all you need is to just speak into the receiver.
JENNINGS: Even in the event of a loss of power... it'd still be functional. That's the real genius about it.
+	So it's basically a hotline?
-
JENNINGS: It's not "basically a hotline", it is *the* Hotline!
JENNINGS: I do wonder where it leads. If it's in the basement, I'd surmise it connects directly to Central. Headquarter higher-ups.
ERGOMANIA: Bingo.
JENNINGS: Maybe you don't even need to repair that modern proprietary MetPhone horseshit. Just use the Class B! Way better--and more reliable--technology! Wish we'd still make those. How far has this company fallen...
+   Sounds cool. The only problem is it's locked in a cabinet.
    JENNINGS: Ah. Well, this *is* the Eastern Exchange Building, the Dusty And Doomed Exchange Building where they've transfered a whole bunch of archival documents from all over to prepare for the handover into privatisation. 
    JENNINGS: ...bloody privatisation...
    JENNINGS: Anyway, bet you'd find something useful in the Archives level.
+   I think I'd rather stick with the mordern proprietary MetPhone horseshit.
    JENNINGS: As expected from a clueless young girl...
    JENNINGS: Well, suit yourself. But if you change your mind, there should be manuals and documentation lying around in the Archives level.
-
JENNINGS: Ah... the Class B...
JENNINGS: I wish I could go see with my own eyes, unfortunately I am hard at work... Ah...
JENNINGS: Anyway. Did you find anything else that's interesting?
->jennings_questionsMenu

== jennings_petradeskInspected ==
// jennings DOESNT hide petrakis' existence. he doesnt seem to have noticed shes gone missing though...
JENNINGS: Whose desk? Ah! That's Petrakis' desk. Where did she go? There's much work to do.
PARANOIA: A third person?
+	Petrakis?
-
JENNINGS: Yes! The third and final member of my team... we're awfully reduced compared to the glory days of telecom, I used to lead a team of dozens! Dozens!
JENNINGS: So many good people laid off in preparation for this... preposterous privatisation scheme. All replaced by cheap contractors like yourself.
+	...Thanks. Who's Petrakis?
+	Okay. I'd like to know more about Petrakis, though.
-
JENNINGS: Petrakis is my assistant researcher! More competent than Choi, for sure, although these days she's been quite flaky.
JENNINGS: Well, maybe it's good that she's gone. Choi and her were probably scheming against me behind my back. Thick as thieves, those two.
{ choi_questionsMenu.choi_petradeskDeflect: PARANOIA: Oh. This must be what Choi is lying about. It's gonna feel good to let him know... that we know. }
+	{ choi_questionsMenu.choi_petradeskDeflect } Wait, Choi pretended he didn't know her.
	JENNINGS: Typical. He always was envious of her and he'd do anything for a promotion.
	++	Looks like I'll have to ask Choi about her.
+	Looks like I'll have to ask Choi about her.
-
JENNINGS: Of course! Of course. Now whether he'll be candid about her... but I digress.
JENNINGS: More questions? I really need to get back to my work.
->jennings_questionsMenu

== jennings_Completed ==
JENNINGS: Is it done yet?
+	Done and dusted.
	JENNINGS: Ah, took you long enough. 
+	Yeah. With the Class B, too. // if you went the antique phone route
	JENNINGS: Excellent work.
-
JENNINGS: Now to see how long they take to restore power...
->DONE

== metphone_Inspect ==
Someone tampered with the access panel...
->DONE

== oldphone_Inspect ==
It's a very old phone. Says "1887 - MTEC" on it.
->DONE

== petradesk_Inspect ==
This desk is weird. Why is it so messy? Why are there so many documents on it?
->DONE