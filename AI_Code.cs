class Population
{
    Individual[] individuals;


    public Population(int populationSize, bool initialize)
    {
        individauls = new Individual[populationSize];
        if(initialize)
        {
            for(int i = 0; i < size(); i++)
            {
                Individual newIndividual = new Individual();
                newIndividual.generateIndividual();
                saveIndividual(i, newIndividual);
            }
        }
    }

    public Individual getIndividual(int index)
    {
        return individauls[index];
    }

    public Individual getFittest()
    {
        Individual fittest = individauls[0];
        for(int i = 0; i < size(); i++)
        {
            if(fittest.getFitness() <= individauls[i].getFitness())
            {
                fittest = getIndividual(i);
            }
        }

        return fittest;
    }

    public int size()
    {
        return individauls.Length;
    }

    public void saveIndividual(int index, Individual indiv)
    {
        individauls[index] = indiv;
    }


}


class Individual
{
    Random rand = new Random();
    static int defaultGeneLength = 64;
    private byte[] genes = new byte[defaultGeneLength];
    private int fitness = 0;

    public void generateIndividual()
    {
        for(int i = 0; i < size(); i++)
        {
            byte gene = (byte) rand.Next();
            genes[i] = gene;
        }
    }

    public void setDefaultGeneLength(int length)
    {
        defaultGeneLength = length;
    }

    public byte getGene(int index)
    {
        return genes[index];
    }

    public void setGene(int index, byte value)
    {
        genes[index] = value;
        fitness = 0;
    }

    public int size()
    {
        return genes.Length;
    }


    public int getGetFitness
    {
        if(fitness == 0)
        {
            fitness = FitnessCals.getFitness(this);
        }
        return fitness;
    }

    public string toString()
    {
        string geneString = "";
        for(int i = 0; i < size(); i++)
        {
            geneString += getGene(i);
        }
        return geneString;
    }

}




class Algorithm
{
    Random rand = new Random();
    readonly double uniformRate = 0.5;
    readonly double mutationRate = 0.015;
    readonly  int tournamentSize = 5;
    readonly bool elitism = true;

    public Population evolvePopulation(Population pop)
    {
        Population newPopulation = new Population(pop.size(), false);

        if(elitism)
        {
            newPopulation.saveIndividual(0, pop.getFittest());
        }
        int elitismOffset;

        if(elitism)
        {
            elitismOffset = 1;
        }
        else
        {
            elitismOffset = 0;
        }

        for(int i = elitismOffset; i < pop.size(); i++)
        {
            Individual indiv1 = tournamentSelection(pop);
            Individual indiv2 = tournamentSelection(pop);
            Individual newIndiv = crossover(indiv1, indiv2);
            newPopulation.saveIndividual(i, newIndiv);
        }

        for(int i = elitismOffset; i < pop.size(); i++)
        {
            mutate(newPopulation.getIndividual(i));
        }

        return newPopulation;

    }

    private Individual crossover(Individual indiv1, Individual indiv2)
    {
        Individual newSol = new Individual();

        for(int i = 0; i < indiv1.size(); i++)
        {
            if(rand.Next() <= uniformRate)
            {
                newSol.setGene(i, indiv1.getGene(i));
            }
            else
            {
                newSol.setGene(i, indiv2.getGene(i));
            }
        }

        return newSol;
    }

    private void mutate(Individual indiv)
    {
        for(int i = 0; i < indiv.size(); i++)
        {
            if(rand.Next() <= mutationRate)
            {
                byte gene = (byte) rand.Next();
                indiv.setGene(i, gene);
            }
        }
    }

    private Individual tournamentSelection(Population pop)
    {
        Population tournament = new Population(tournamentSize, false);
        for(int i = 0; i < tournamentSize; i++)
        {
            int randomId = (int) rand.Next() * pop.size();
            tournament.saveIndividual(i, pop.getIndividual(randomId));
        }

        Individual fittest = tournament.getFittest();
        return fitness;
    }
}


class FitnessCalc
{
    byte[] solution = new byte[46];

    public void setSolution(byte[] newSolution)
    {
        solution = newSolution;
    }


    // void setSolution(string newSolution)
    // {
    //     solution = new byte[newSolution.Length];

    //     for(int i = 0; i < newSolution.Length; i++)
    //     {

    //     }

    // }


    int getFitness(Individual individual)
    {
        int fitness = 0;
        // Loop through our individuals genes and compare them to our cadidates
        for (int i = 0; i < individual.size() && i < solution.Length; i++) {
            if (individual.getGene(i) == solution[i]) {
                fitness++;
            }
        }
        return fitness;
    }


    // Get optimum fitness
    int getMaxFitness()
    {
        int maxFitness = solution.length;
        return maxFitness;
    }

}





class MainClass {
  public static void Main (string[] args)
  {
    FitnessCalc.setSolution(1111000000000000000000000000000000000000000000000000000000001111);

    // Create an initial population
    Population myPop = new Population(50, true);

    // Evolve our population until we reach an optimum solution
    int generationCount = 0;
    while (myPop.getFittest().getFitness() < FitnessCalc.getMaxFitness()) {
    generationCount++;

    Console.WriteLine("Generation: " + generationCount + " Fittest: " + myPop.getFittest().getFitness());
    myPop = Algorithm.evolvePopulation(myPop);

    Console.WriteLine("Solution found!");
    Console.WriteLine("Generation: " + generationCount);
    Console.WriteLine("Genes:");
    Console.WriteLine(myPop.getFittest());
    
  }



}