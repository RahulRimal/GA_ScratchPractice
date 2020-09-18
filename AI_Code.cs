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


    }




}