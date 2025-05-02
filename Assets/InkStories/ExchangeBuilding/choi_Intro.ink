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

