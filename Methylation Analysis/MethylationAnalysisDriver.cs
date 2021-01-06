//////////////////////////////////////////////////////////////////////////////////////
//
//  Project: Project 3 - DNAAnalysis
//  File Name: MethylationAnalysisDriver.cs
//  Description: Generates a Methylation report based on a DNA file 
//  Course: CSCI 3230-001 - Algorithms
//  Author: Seth Norton, nortonsp@etsu.edu
//  Created: May 19, 2020
//  Copyright: Seth Norton, 2020
//
///////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The name space for the Methylation analysis
/// </summary>
namespace Methylation
{
    /// <summary>
    /// The class which contains the program which runs a Methylation analysis for the supplied DNA file
    /// </summary>
    public class MethylationAnalysisDriver
    {
        
        /// <summary>
        /// A struct which contains the rs info for RS numbers
        /// </summary>
        struct DNAInfo
        {
            /// <summary>
            /// The rsid for a gene
            /// </summary>
            public string RSID{get;set;}
            /// <summary>
            /// The chromosome of the gene/rs number
            /// </summary>
            public int Chromosome { get; set; }
            /// <summary>
            /// The position of the rs number/gene
            /// </summary>
            public int Position { get; set; }
            /// <summary>
            /// The first allele of the gene/rs number
            /// </summary>
            public char AlleleOne { get; set; }
            /// <summary>
            /// The second allele of the gene/rs number
            /// </summary>
            public char AlleleTwo { get; set; }
            /// <summary>
            /// Shows if the rs number in the text file has a mutation
            /// </summary>
            public string Mutation { get; set; }


        }//end struct DNAInfo
        /// <summary>
        /// The main method which runs the Methylation report of the rs numbers
        /// </summary>
        /// <param name="args">The arguments supplied to the program</param>
        public static void Main(string[] args)
        {

            List<DNAInfo> DNAInfoList = new List<DNAInfo>();    //a list of all the DNA info
            DNAInfo da = new DNAInfo();                         //stores the DNA info for an rs number
            string DNAInfo = "";                                //a string which gets each line of the DNA file
            int counter = 0;                                    //a counter which is used to determine when we should parse rs numbers
            DNAInfo = Console.ReadLine();
            while (DNAInfo != null)
            {
                DNAInfo = Console.ReadLine();
                if (counter > 0 && DNAInfo != null)
                {
                    da.RSID = DNAInfo.Split('\t')[0];
                    da.Chromosome = Int32.Parse(DNAInfo.Split('\t')[1]);
                    da.Position = Int32.Parse(DNAInfo.Split('\t')[2]);
                    da.AlleleOne = DNAInfo.Split('\t')[3].ToCharArray()[0];
                    da.AlleleTwo = DNAInfo.Split('\t')[4].ToCharArray()[0];
                    DNAInfoList.Add(da);
                }//end if (counter > 0 && DNAInfo != null)
                if (counter == 0 && DNAInfo[0] != '#')
                    counter++;

            }//end while (DNAInfo != null)
            //Add the associated allelles
            Dictionary<string, string> AlelleDictionary = new Dictionary<string, string>    //a dictionary which contains the dominant alleles of a rs number
            {
                {"rs4680", "G;G" },
                {"rs4633", "C;C" },
                {"rs769224", "G;G" },
                {"rs1544410", "C;C" },
                {"rs731236", "T;T" },
                {"rs6323", "A;A" },
                {"rs3741049", "G;G" },
                {"rs1801133", "G;G" },
                {"rs2066470", "T;T" },
                {"rs1801131", "C;C" },
                {"rs1805087", "A;A" },
                {"rs1801394", "A;A" },
                {"rs10380", "C;C" },
                {"rs162036", "A;A" },
                {"rs2287780", "C;C" },
                {"rs1802059", "G;G" },
                {"rs567754", "C;C" },
                {"rs617219", "A;A" },
                {"rs651852", "C;C" },
                {"rs819147", "T;T" },
                {"rs819134", "A;A" },
                {"rs819171", "T;T" },
                {"rs234706", "G;G" },
                {"rs1801181", "G;G" },
                {"rs2298758", "G;G" },
                {"rs1979277","G;G" }

            };
            Dictionary<string, string> VariationDictionary = new Dictionary<string, string>()   //a dictionary containing the variation of particular rs numbers
            {
                {"rs4680", "COMT V158M" },
                {"rs4633", "COMT H62H" },
                {"rs769224", "COMT P199" },
                {"rs1544410", "VDR Bsm" },
                {"rs731236", "VDR Taq" },
                {"rs6323", "MAO A R297R" },
                {"rs3741049", "ACAT1-02" },
                {"rs1801133", "MTHFR C677T" },
                {"rs2066470", "MTHFR 03 P39P" },
                {"rs1801131", "MTHFR A1298C" },
                {"rs1805087", "MTR A2756G" },
                {"rs1801394", "MTRR A66G" },
                {"rs10380", "MTRR H595Y" },
                {"rs162036", "MTRR K350A" },
                {"rs2287780", "MTRR R415T" },
                {"rs1802059", "MTRR A664A" },
                {"rs567754", "BHMT-02" },
                {"rs617219", "BHMT-04" },
                {"rs651852", "BHMT-08" },
                {"rs819147", "AHCY-01" },
                {"rs819134", "AHCY-02" },
                {"rs819171", "AHCY-19" },
                {"rs234706", "CBS C699T" },
                {"rs1801181", "CBS A360A" },
                {"rs2298758", "CBS N212N" },
                {"rs1979277","SHMT1 C1420T" }
            };
            //the string which contains the report of the methylation analysis
            string report = "Gene And Variation".PadRight(20).PadLeft(20) + "|".PadRight(5) + "rsID".PadRight(10) + "|".PadRight(5) + "Alleles".PadRight(10) + "|".PadRight(5) + "Result".PadRight(10) + "|".PadRight(5) + "Mutation".PadRight(10) + "|".PadRight(5) + "Type of Mutation".PadRight(28) + "|".PadRight(5) + "\n";





            List<string> valuesUsed = new List<string>();   //the values used for the methylation analysis
            //Scan through the list of RSID's and find if one matches the rsID in the Allele Dictionary
            for (int i = 0; i < DNAInfoList.Count; i++)
            {
                if (AlelleDictionary.ContainsKey(DNAInfoList[i].RSID))
                {

                    //find if the alleles in DNAInfoList[i] are mutated from the ones in the Alelle Dictionary
                    string AlleleOne = "";
                    string AlleleTwo = "";
                    string Mutation = "";
                    string MutationType = "";
                    valuesUsed.Add(DNAInfoList[i].RSID);
                    AlelleDictionary.TryGetValue(DNAInfoList[i].RSID, out AlleleOne);
                    AlleleTwo = AlleleOne.Split(';')[1];
                    AlleleOne = AlleleOne.Split(';')[0];
                    if (DNAInfoList[i].AlleleOne == AlleleOne[0])
                    {
                        if(DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                        {
                            Mutation = "-/-";   //No mutation
                            MutationType = "No mutation";
                        }//end if(DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                        else
                        {
                            Mutation = "-/+";
                            MutationType = "Single Mutation: Allele Two";
                        }//end else(DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                    }//end if (DNAInfoList[i].AlleleOne == AlleleOne[0])
                    else
                    {
                        if (DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                        {
                            Mutation = "+/-";
                            MutationType = "Single Mutation: Allele One";
                        }//end if(DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                        else
                        {
                            Mutation = "+/+";
                            MutationType = "Double Mutation";
                        }//end else(DNAInfoList[i].AlleleTwo == AlleleTwo[0])
                    }//end else(DNAInfoList[i].AlleleOne == AlleleOne[0])
                    string variation = "";  //stores the gene variation of the rs number
                    VariationDictionary.TryGetValue(DNAInfoList[i].RSID, out variation);
                    report += variation.PadRight(20).PadLeft(20) + "|".PadRight(5) + DNAInfoList[i].RSID.PadRight(10) + "|".PadRight(5) + DNAInfoList[i].AlleleOne.ToString().PadRight(10) + "|".PadRight(5) + DNAInfoList[i].AlleleTwo.ToString().PadRight(10) + "|".PadRight(5) + Mutation.PadRight(10) + "|".PadRight(5) + MutationType.PadRight(28) + "|".PadRight(5) + "\n";
                }//end if (AlelleDictionary.ContainsKey(DNAInfoList[i].RSID))
            }//end for (int i = 0; i < DNAInfoList.Count; i++)
            string RSNumbersNotFound = "";  //string which contains the rs numbers not found in the DNA file
            foreach(KeyValuePair<string, string> entry in VariationDictionary)
            {
                if(!valuesUsed.Contains(entry.Key))
                {
                    RSNumbersNotFound += entry.Key + " (" + entry.Value.Trim() +")" + ", ";
                }//end if(!valuesUsed.Contains(entry.Key))
            }//end  foreach(KeyValuePair<string, string> entry in VariationDictionary)
            Console.WriteLine(report);
            Console.WriteLine();
            Console.WriteLine("RS Numbers and Genes not found in DNA file: ");
            Console.WriteLine(RSNumbersNotFound);
            Console.ReadLine();
        }//end public static void Main(string[] args)
    }//end public class MethylationAnalysisDriver
}//end name space Methylation
