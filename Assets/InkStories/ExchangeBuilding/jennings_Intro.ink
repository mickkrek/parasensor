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

