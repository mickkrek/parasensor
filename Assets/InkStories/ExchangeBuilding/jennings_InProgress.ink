== jennings_InProgress ==
// leading into the jennings question hub
JENNINGS: What now? Keep it short. I'm very busy.
-> jennings_questionsMenu

== jennings_questionsMenu ==
// questions hub that updates as you do things. variables tbd depending on how we wanna implement this stuff.
+	{ metphone_Inspect } I've checked on the phone. Someone tampered with it. ->jennings_metphoneInspected
+   { oldphone_Inspect } I found an antique phone in the basement. { choi_oldphone_Inspected: Choi told me you could tell me about it. | Would you know anything about it? } ->jennings_oldphoneInspected
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
{ choi_petradeskDeflect: PARANOIA: Oh. This must be what Choi is lying about. It's gonna feel good to let him know... that we know. }
+	{ choi_petradeskDeflect } Wait, Choi pretended he didn't know her.
	JENNINGS: Typical. He always was envious of her and he'd do anything for a promotion.
	++	Looks like I'll have to ask Choi about her.
+	Looks like I'll have to ask Choi about her.
-
JENNINGS: Of course! Of course. Now whether he'll be candid about her... but I digress.
JENNINGS: More questions? I really need to get back to my work.
->jennings_questionsMenu
