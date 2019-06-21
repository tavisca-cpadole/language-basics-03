using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            List<int> pr = protein.ToList();
            List<int> ca = carbs.ToList();
            List<int> fa = fat.ToList();
            int[] calorie = new int[protein.Length];
            calorie = CalculateCalorie(protein,carbs,fat);

            List<int> cal = calorie.ToList();
            int[] answer = new int[dietPlans.Length];
            for (int i=0;i<dietPlans.Length;i++) {
                if (dietPlans[i]=="") {
                    answer[i] = 0;
                }
                else if (dietPlans[i].Length == 1)
                {
                    if (dietPlans[i] == "t")
                        answer[i] = cal.IndexOf(cal.Min());
                    else if (dietPlans[i] == "c")
                        answer[i] = ca.IndexOf(ca.Min());
                    else if (dietPlans[i] == "p")
                        answer[i] = pr.IndexOf(pr.Min());
                    else if (dietPlans[i] == "f")
                        answer[i] = fa.IndexOf(fa.Min());
                    else if (dietPlans[i] == "T")
                        answer[i] = cal.IndexOf(cal.Max());
                    else if (dietPlans[i] == "C")
                        answer[i] = ca.IndexOf(ca.Max());
                    else if (dietPlans[i] == "P")
                        answer[i] = pr.IndexOf(pr.Max());
                    else if (dietPlans[i] == "F")
                        answer[i] = fa.IndexOf(fa.Max());
                }
                else {
                    char[] arr1 = dietPlans[i].ToCharArray();
                    int[] indx= { -1};
                    for (int j = 0; j < arr1.Length; j++) {
                        if (arr1[j] == 't') {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(cal, cal.Min()) == 1)
                                {
                                    answer[i] = cal.IndexOf(cal.Min());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(cal, cal.Min());
                                }
                            }
                            else {

                                var list = cal.Skip(indx[0]).Take(indx[indx.Count()-1] - indx[0] + 1).ToList();
                                var max = list.Min();

                                if (FetchCount(cal, max) == 1)
                                {
                                    answer[i] = cal.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(cal, cal[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'p')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(pr, pr.Min()) == 1)
                                {
                                    answer[i] = pr.IndexOf(pr.Min());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(pr, pr.Min());
                                }
                            }
                            else
                            {
                                var list = pr.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Min();
                                if (FetchCount(pr, max) == 1)
                                {
                                    answer[i] = pr.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(pr, pr[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'f')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(fa, fa.Min()) == 1)
                                {
                                    answer[i] = fa.IndexOf(fa.Min());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(fa, fa.Min());
                                }
                            }
                            else
                            {
                                var list = fa.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Min();
                                if (FetchCount(fa, max) == 1)
                                {
                                    answer[i] = fa.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(fa, fa[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'c')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(ca, ca.Min()) == 1)
                                {
                                    answer[i] = ca.IndexOf(ca.Min());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(ca, ca.Min());
                                }
                            }
                            else
                            {
                                var list = ca.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Min();
                                if (FetchCount(ca, max) == 1)
                                {       
                                    answer[i] = ca.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(ca, ca[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'T')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(cal, cal.Max()) == 1)
                                {
                                    answer[i] = cal.IndexOf(cal.Max());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(cal, cal.Max());
                                }
                            }
                            else
                            {
                                var list = cal.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Max();
                                if (FetchCount(cal, max) == 1)
                                {
                                    answer[i] = cal.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(cal, cal[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'P')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(pr, pr.Max()) == 1)
                                {
                                    answer[i] = pr.IndexOf(pr.Max());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(pr, pr.Max());
                                }
                            }
                            else
                            {
                                var list = pr.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Max();
                                if (FetchCount(pr, max) == 1)
                                {
                                    answer[i] = pr.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(pr, pr[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'F')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(fa, fa.Max()) == 1)
                                {
                                    answer[i] = fa.IndexOf(fa.Max());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(fa, fa.Max());
                                }
                            }
                            else
                            {
                                var list = fa.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Max();
                                if (FetchCount(fa, max) == 1)
                                {
                                    answer[i] = fa.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(fa, fa[indx[0]]);
                                }
                            }
                        }
                        else if (arr1[j] == 'C')
                        {
                            if (indx.Length == 1)
                            {
                                if (FetchCount(ca, ca.Max()) == 1)
                                {
                                    answer[i] = ca.IndexOf(ca.Max());
                                    break;
                                }
                                else
                                {
                                    indx = FetchIndex(ca, ca.Max());
                                }
                            }
                            else
                            {
                                var list = ca.Skip(indx[0]).Take(indx[indx.Count() - 1] - indx[0] + 1).ToList();
                                var max = list.Max();
                                if (FetchCount(ca, max) == 1)
                                {
                                    answer[i] = ca.IndexOf(max);
                                    break;
                                }
                                else
                                {
                                    if (j == arr1.Length - 1)
                                    {
                                        answer[i] = indx[0];
                                        break;
                                    }
                                    indx = FetchIndex(ca, ca[indx[0]]);
                                }
                            }
                        }
                    }
                }
            }
            return answer;
        }

        public static int[] FetchIndex(List<int> temp,int y) {           
            var allIndexes = temp
            .Select((t, i) => new { Index = i, Text = t })
            .Where(p => p.Text == y)
            .Select(p => p.Index);
            int j = 0;
            int[] arr = new int[allIndexes.Count()];
            foreach (var i in allIndexes)
            {
                arr[j]= i;
                j++;
            }
            return arr;
        }

        public static int FetchCount(List<int> temp,int y) {
            IList<int> words = temp;

            return words.Where(x => x.Equals(y)).Count();
        }
        public static int[] CalculateCalorie(int[] protein, int[] carbs, int[] fat) {
            int[] temp = new int[protein.Length];
            for (int i=0;i<protein.Length;i++) {
                temp[i]=protein[i]*5+carbs[i]*5+fat[i]*9;
            }
            return temp;
        }
    }
}
