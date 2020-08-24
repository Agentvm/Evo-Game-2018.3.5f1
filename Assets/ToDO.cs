/*
 * 
 * 
 * 
 * 
 * 
 * 

 --- Designfragen ----------------------
 
 - (Schwerwiegend!) Komplexitätswelle: Jede Kleinigkeit wie etwa der Nährstoffbedarf einer Pflanze will festgelegt werden, hängt aber auch
    von vielen anderen Faktoren ab, die im Zweifelsfall alle ich festlegen muss.
    -> zunächst mal öffentlich in GameVariables festlegen, beim Spielen ausprobieren und anpassen.
    -> oder vielleicht doch nicht, denn das Alles wird sich ständig verändern
    -> Aber gerade dann könnte es sinnvoll sein, erst eine stabile Version herzustellen, bevor man am Ende vor einem wirren Knäuel an kaputten
        Beziehungen steht.

 - (Schwerwiegend) Wann werden neue Traits/Abilities in Nachkommen sichtbar? (zufällig, Techtree, Anzahl aktivierter Gene, bestimmte Genkombinationen)
                   Kann das Kriterium dafür in der Klasse der Ability untergebracht werden, ähnlich wie in der IntensityFunctionCollection?

 - Wird die Länge eines Genoms erhöht?

 - Wie passen Metaaktionen wie Sterben/Fortpflanzen in das Konzept einer Ability? Muss evt ein anderes Konzept genutzt werden?
    -> Da eine Ability Zugriff auf den _character hat, kann sie so ziemlich alles lostreten, das sollte kein Problem sein.

 - scaling thoughts (2 ^ Intensity scheint eine gute Grundlage für einen konstanten Wert in einer Ability zu sein )
 - (check) Color Problem (Color Genom should mirror certain genes of the main Genom)
 - Juvenile Problem (start at 1/10 maxSize, then progress with each game tick)


 
 - (Schwerwiegend) Zeitproblem: Evolutive Entwicklungen zeigen sich erst im Verlauf mehrerer hundert Jahre, aber man will auch den Tagesablauf eines Wesens
    im Stil eines Terrariums sehen können.
    - Schlaf: Ein Trait? Wann ist Nacht?
    - Wie oft werden Abilities gecallt? Alle auf einmal, oder eine pro Tick?

 - Sinnvoller Spielstart (Spieler wählt Startpunkt und Anfangsausrichtung der Spezies)
 - (Schwerwiegend) Motivierendenes Gameplay (Techtree, Mutationspunkte um Gene gezielt mutieren zu lassen, Individuen von der Fortpflanzung ausschließen)
 - Motivierendes Spielziel (eine intelligente Rasse hervorbringen, die eigene Spezies überleben lassen, ...)
 - Das Spiel angemesses schwierig gestalten (Tipps einblenden, dem Spieler mehr Macht geben)
    - es ist duchaus vorstellbar, dass menschliche Spieler es nicht fertigbringen, eine funktionierende Spezies aufleben zu lassen. Schließlich
      ist das Prinzip der Evolution selbst ein Optimierungsalgorithmus, der erst einmal übertroffen werden will.


 --- ToDo --------------------------------------------------------------------------------------------------------------------------------------------------
 
 - (check) Generalize Functions in IntensityFunctionCollection
 - (check) Use Instantiate instead of FindObjectsWithTag
 - (check) Debug GameVariables.getTraits // Warning does never fire
 - (check) implement checks for AbilityData integrity
 - Corellation List: Which Abilities need which Traits? Or find a way to specify for each Ability the Traits that it needs
 - Implement a manifestTrait () - function that allows Trait Correlation between scattered Traits
 
 --- ToDo: The Road to Life
 - Add a measure of life energy to the Character class. It is depleted for growth and filled by photosynthesis or nourishment
 - Make Plants fight for light: Size means Height to put others below you and LeavesDensity means both increasing income and shadowing others
 - Make a Baby
 - Invite the Reaper for Tea
 - Project Evergreen: Make Plants
    - Sow Seeds
    - Sunligth/Sunblock mechanic
    - Water mechanic (Weather is not to be implemented until later)
    - Nutrient mechanic (complete with dead animals and poop)
 - Project Herbivore: Make Animals that live of plants
 - Project Carnivore: Make Animals that live of Animals that live of plants (and of unfortunate accidents)
 - Give them a home: Terretories
 - Up Above and Down Below: Make Water (River and Lake) and Air (Treetop and Cliff)
 - Awake the Spirits of the Planet (Day/Night, Seasons, Heat & Cold, Clouds, Rain, Katasthrophes)
 - Bonus: Mutating Illnesses

 --- ToDo: Übersicht
 - Edit the GameVariables.tick() behaviour so more complicated actions are possible. Setting a current goal would be nice.
 - (check) Make species distiguishable by color
 - Make species distinguishable by icon (sprite). (Ein Ansatz könnte sein, Individuen mit wenigen, einigen und vielen aktivierten Genen im Genom (Einsen
    im Genome) mit jeweils einem anderen Sprite zu kennzeichnen.)
 - Use Interface IDisplayable to make Individuals displayable, then change InfoTag to display their content
 - Use this to show some information about the traits and features of the Individuum the player is hovering over.
 - Make selecting an Individuum possible
 - Implement the "Species" Class that summarizes Individuals. (Vielleicht kann Species auch ein Attribut sein. Dies könnte in Form eines Verweises auf das
    Begründer-Individuum der betreffenden Spezies realisiert werden.)
     - When displaying an Individuum, show the difference to it's species (genome-wise, Traits,
        Abilities).
     - When selecting an individuum, also highlight all of their species members and hint at their childs and parents with tiny arrows
 - (To What use?) Use Tags to sort Individuals into groups. Use Trait based values such as Herbivore, Carnivore, Landliving, Waterliving, Airliving
 - (Bonus) Make an EvolutionTree Document
 
 --- ToDo: Nachforschung
 - Make a fancy genom viewer
 - Make Traits selectable and display the corresponding genes
 - Genes that have high importance (correspond to many Traits), should be enlarged
 - Make long Genome Strings displayable by scrolling movement
 - Give the player opportunity to favorite Individuals and monitor theirs and their descendants fate


 --- Verbesserungen -----------------------------
 
 - Mutationsrate sollte keine Eigenschaft der Character-Klasse sein, da sie zu einer Zeit gebraucht wird, zu der der betreffende Character noch nicht
    initialisiert wurde. Stattdessen die Rate als Genome.mutate (float rate) übergeben.
 - Make a Lerp Version or -Option of SlerpCoroutine
 - (fair enough) fix camera scrolling movement
 - Genome Convenience Functions for adding multiple interwoven traits:
 - Trait placement in genome: Make an addTraits function that lets the user specify with which Trait and to which degree the added Trait should overlap


  --- Ideen -----------------------------
 
 - TraitIntensityFunction, die entweder <0, 0 oder >0 ergibt, je nachdem ob die Anzahl der 1-gene die Anzahl der 0-gene unterschreitet, ihr gleicht,
    oder sie überschreitet.
 - Abwandlung des vorangehenden Punktes: Gezählt werden die Gene abhängig ihrer Stelle im Genom. Zunächst werden alle Stellen durchnummeriert, danach die
    ungeraden Stellen gegen die geraden aufgewogen. Ist die Anzahl der aktivierten Gene in den geraden Stellen höher, gleich oder geringer als in den,
    ungeraden ergibt sich eine von drei Möglichkeiten. Dies hat den Vorteil, dass andere Traits im gleichen Genomabschnitt kaum beeinflusst werden. Die
    Gesamtanzahl der aktivierten Gene kann hoch oder niedrig sein, solange nur eine der geraden oder ungeraden Stellen nicht aktiviert wird, ist
    das Ergebnis für beide Traits offen, sie können voneinander unabhängig eine vorteilhafte Ausprägung annehmen.
 - (check) Use Delegates to make Character Abilities work
 - Eine Liste an Vorlieben, die Essgewohnheiten eines Character zusammenfasst. Integerwerte, die durch Gaussmutation bei der Vererbung verändert
    werden.
 - Let every Charakter have a second, "preferences" genom, that can be altered aswell. This is used to evolve feeding and wandering routines.
 - Gleiche (grundlegende) Traits wie "MaxSize" immer an die gleiche Position im Genom legen. Eventuell Trait.Position wieder einführen.
 - Initialisierung von grundlegenden Zellen-Charakteren mit einem 1010101010101010-Genom + Mutation durchführen. Sorgt für ausgeglichenere Charaktere zu
    Beginn der Evolution
 



 *
 * 
 */