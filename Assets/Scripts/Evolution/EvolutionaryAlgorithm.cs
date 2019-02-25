// Global: Selektionsdruck
// 
// 
// 
// 



public class EvolutionaryAlgorithm
{
    /*

    genotype_size = 100 // the same as the number of possible objects in the kapsack.
                        // sets the size for every member of any population
    private float mutation_scale = 1.0
    inlude_parents_inSelection = False
    do_recombination = False

    
    
    void set_mutation_scale (self, scale ):
        ''' (re)set the scale of random changes made to the child population
            each iteration '''
        self.mutation_scale = scale

    void set_recombination (self, answer ):
        self.do_recombination = answer


    void recombine_parent_population (self ):
        ''' using a 1-point crossover, this function recombines two random parents '''
        // print ("in RECOMBINATION: ------------------------")
        self.child_pop = []
        for _ in range (self.offspring_count ):

            // randomly determine the two partners and the crossover point
            crossover_point = np.random.choice (len(self.parent_pop[0] )  )
            partner_1 = self.parent_pop[np.random.choice (len(self.parent_pop )  )]
            partner_2 = self.parent_pop[np.random.choice (len(self.parent_pop )  )] // incest awaits

            // append the newly created child to the child population
            new_genotype = deepcopy (partner_1[:crossover_point] + partner_2[(crossover_point):] )
            self.child_pop.append (new_genotype)


    void repopulate (self):
        ''' builds new generation from parents, through recombination
        or random selection '''
        self.child_pop = []

        if self.do_recombination:
            self.recombine_parent_population ()
        else: // chosing random parent?
            for _ in range (self.offspring_count ):
                self.child_pop.append (deepcopy (self.parent_pop[np.random.choice (len(self.parent_pop ) )] )  )


    void mutate_child_pop (self ):
        ''' Change child population based on chance '''
        nb = 0
        positions = []

        for x in range(len(self.child_pop )  ):
            for y in range (len (self.child_pop[0] )  ):
                if np.random.choice (math.ceil (len (self.child_pop[0] ) * (1/self.mutation_scale) )  ) == 0:
                    if self.child_pop[x][y] == 0:
                        self.child_pop[x][y] = 1
                    else:
                        self.child_pop[x][y] = 0
                    positions.append ((x,y))
                    nb +=1
                    

    def start_evolution (self, do_print = False ):
        ''' (x +/, y) evolutionary algorithm  '''
        if self.problem is None:
            print ("Use set_problem () first.")
            return

        sign = ", "
        if self.inlude_parents_inSelection:
            sign = " + "
        print ( "starting a ("+str(str(self.pop_size))+ sign +str(str(self.offspring_count))+
                ") evolution with a mutation_scale of "+str(self.mutation_scale)+
                " and "+str(self.iterations)+ " iterations.")
        if self.do_recombination:
            print ("Recombination is active.")
        else:
            print ("No recombination is used")
        //print ("start population: " + str (self.get_current_solution (False )  ) +"\n"  )
        //print ("start population: " + str (self.parent_pop ) +"\n"  )

        // list of fitness values for printing
        self.max_fitness_values.append (self.get_max_fitness (self.parent_pop ) ) // 1st population fitness

        generation_nb = 0

        // start iterating
        for i in range (self.iterations ):
            generation_nb += 1

            print ("\n")

            // new population is made
            self.repopulate ()

            // and mutated
            self.mutate_child_pop ()

            // the best members of the populations are chosen as new parents
            self.select_from_population ()

            // save the max fitness of this generation for plotting
            self.max_fitness_values.append (self.get_max_fitness (self.parent_pop ) )


// start of program
if __name__ == "__main__":

    print ( "Knapsack Problem\nGegeben:" )
    print ( "Ein Rucksack mit maximaler Tragekapazität (Gewicht)\n"+
            "Gegenstände, je mit Wert und Gewicht, die zu transportieren sind\nZiel:" )
    print ( "Befülle den Rücksack so, dass möglichst viel Wert transportiert wird\n" )
    print ("\n--------------------------------------------")

    for _ in range (1):
        // configure the algorithm
        evo = EvolutionaryKnapsackAlgorithm ()
        evo.set_problem (generate_knapsack_problem (3 )  )
        evo.set_iteration_count (1000 )
        evo.set_algorithm_shape (10, False, 100 )
        evo.set_recombination (True )
        evo.start_evolution ()

        // print the result
        solution, fitness_vals = evo.get_current_solution ()
        print("------------------------------------\n\n\nA Solution has been found.")
        sum_weights = 0.0
        for s in solution:
            sum_weights += s[1]
        print ("fitness: " + str (fitness_vals[-1] ) + ', Rucksackgewicht: ' +
                str (sum_weights ) + " / " + str (evo.knapsack_size )  )
        print ("\ndetailed solution (value, weight):")
        print (solution)

    // plot the fitness of the best solution per iteration
    plot_fitness (fitness_vals, 1 )
    plt.show ()

 */   
}