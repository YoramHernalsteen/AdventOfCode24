namespace AdventOfCode24.AdventDays;

public static class Day9
{
    public static long Solve()
    {
        var data = Core.ConvertFileToListInt();
        var fileSystem = new List<int>();
        var id = 0;
        for (var i = 0; i < data.Count; i+=2)
        {
            
            for (var j = 0; j < data[i]; j++)
            {
                fileSystem.Add(id);
            }
            if (i + 1 < data.Count)
            {
                for (var j = 0; j < data[i + 1]; j++)
                {
                    fileSystem.Add(-1);
                }
            }

            id++;
        }

        var i1 = 0;
        var i2 = fileSystem.Count - 1;

        while (i1 < i2)
        {
            if (fileSystem[i1] != -1)
            {
                i1++;
                continue;
            }
            if (fileSystem[i2] == -1)
            {
                i2--;
                continue;
            }
            
            (fileSystem[i1], fileSystem[i2]) = (fileSystem[i2], fileSystem[i1]);
        }
        
        fileSystem.RemoveAll(x => x == -1);
        return fileSystem.Select((x, i) => (long)(x * i)).Sum();
    }

    public static long SolveExtra()
    {
        var data = Core.ConvertFileToListInt();
        var fileSystem = new List<int>();
        var emptyPlaces = new List<(int, int)>();
        var filledPlaces = new List<(int, int, int)>();
        var id = 0;
        for (var i = 0; i < data.Count; i+=2)
        {
            
            filledPlaces.Add((fileSystem.Count,id, data[i]));
            for (var j = 0; j < data[i]; j++)
            {
                fileSystem.Add(id);
            }
            if (i + 1 < data.Count)
            {
                emptyPlaces.Add((fileSystem.Count, data[i + 1]));
                for (var j = 0; j < data[i + 1]; j++)
                {
                    fileSystem.Add(-1);
                }
            }

            id++;
        }
        
        while (filledPlaces.Count > 0)
        {
            var i = 0;
            var (lastFilledIndex, lastFilledId, lastFilledPlaces) = filledPlaces.Last();
            while (i < filledPlaces.Count && i < emptyPlaces.Count)
            {
                var (firstEmptyIndex, firstEmptyPlaces) = emptyPlaces[i];
                
                if(firstEmptyIndex > lastFilledIndex) break;
                if (firstEmptyPlaces < lastFilledPlaces)
                {
                    i++; 
                    continue;
                }
                for (var j = 0; j < lastFilledPlaces; j++)
                {
                    fileSystem[firstEmptyIndex + j] = lastFilledId;
                    fileSystem[lastFilledIndex + j] = -1;
                }

                if (firstEmptyPlaces == lastFilledPlaces)
                {
                    emptyPlaces.RemoveAt(i);
                }
                else
                {
                    emptyPlaces[i] = (firstEmptyIndex + lastFilledPlaces, firstEmptyPlaces - lastFilledPlaces);
                }
                
                break;
            }
            
            filledPlaces.RemoveAt(filledPlaces.Count - 1);
        }
        return fileSystem.Select((x, i) => x != -1 ? (long)(x * i) : 0).Sum();
    }
}