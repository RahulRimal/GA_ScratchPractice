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


public class Individual
{
    static int defaultGeneLength = 64;
    private byte[] genes = new byte[defaultGeneLength];
    private int fitness = 0;
}