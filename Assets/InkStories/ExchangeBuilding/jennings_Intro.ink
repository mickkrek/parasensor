== jennings_Intro ==
// this checks if you spoke to choi first
{
    - choi_Intro: -> jennings_IntroFirst
    - not choi_Intro: -> choi_IntroFirst
}

ERGOMANIA: Wow, that's some advanced pen spinning. This guy is hard at work.
+ 	\(Extending a hand) Hi, I'm Marisol.
+ 	\(Coughing to get his attention) Excuse me.
-

	= jennings_IntroFirst
	JENNINGS: ...
	+ 	I'm here to fix a phone...?
	+ 	Hello...?
	-
	JENNINGS: ...
	PARANOIA: He's ignoring us. He knows we're not *actually* up to the task. 
	ERGOMANIA: No way. He's just really, really focusing on work.
	+ 	Fine. I'll talk to your colleague.
	+ 	\(Staring intently) ...
		// Turning to Marisol for the first time
		JENNINGS: Quit staring at me like that!
		// Turning back towards the computer and muttering
		JENNINGS: Disrupting my focus like that. Unbelievable.
		// Turning to Marisol again
		JENNINGS: Just... talk to Choi, alright?
		JENNINGS: Choi?
		CHOI: (Sigh) What?
		JENNINGS: That guy. Talk to him. Alright?
	-
	-> DONE

	= choi_IntroFirst
	// Staring at the computer
	JENNINGS: Leave me alone, kid. Busy.
	JENNINGS: Don't ya have a phone to check on?
	-> DONE

