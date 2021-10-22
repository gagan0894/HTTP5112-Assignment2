using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;

namespace HTTP5112_Assignment2.Controllers
{
    public class AssignmentController : ApiController
    {
        /// <summary>
        /// J1 PROBLEM
        /// This webAPI solves a Cell Sell J1 problem from 2005
        /// cemc.math.uwaterloo.ca/contests/computing/2005/stage1/juniorEn.pdf
        /// </summary>
        /// <param name="minDay">Number of daytime minutes</param>
        /// <param name="minEve">Number of evening minutes</param>
        /// <param name="minWeekend">Number of weekend minutes</param>
        /// <example>api/Assignment/CellSell/500/200/300</example>
        /// <returns>
        /// Plan A costs $150
        /// Plan B costs $232
        /// Plan A is cheapest.
        /// </returns>
        [Route("api/Assignment/CellSell/{minDay}/{minEve}/{minWeekend}")]
        [HttpGet]
        public IEnumerable<string> CellSell(int minDay, int minEve, int minWeekend)
        {
            int[] ChargePlanA = { 25, 15, 20 }; //Array to store the charges for Plan A for day, evening and weekend respectively
            int[] ChargePlanB = { 45, 35, 25 }; //Array to store the charges for Plan B for day, evening and weekend respectively

            int FreeMinutesA = 100; //variable to define free minutes in daytime for Plan A
            int FreeMinutesB = 250; //variable to define free minutes in daytime for Plan B

            decimal PlanACost = 0;
            decimal PlanBCost = 0;

            PlanACost = (((minDay - FreeMinutesA) * ChargePlanA[1]) + (minEve * ChargePlanA[1]) + (minWeekend * ChargePlanA[2])) / 100;
            PlanBCost = (((minDay - FreeMinutesB) * ChargePlanB[1]) + (minEve * ChargePlanB[1]) + (minWeekend * ChargePlanB[2])) / 100;

            string Message1 = "Plan A costs $" + PlanACost.ToString();
            string Message2 = "Plan B costs $" + PlanBCost.ToString();
            string Message3 = "";
            if (PlanACost > PlanBCost)
            {
                Message3 = "Plan B is cheapest.";
            }
            else if (PlanBCost > PlanACost)
            {
                Message3 = "Plan A is cheapest.";
            }
            else
            {
                Message3 = "Plan A and B are the same price.";
            }
            return new string[] { Message1, Message2, Message3 };
        }

        /// <summary>
        /// J2 PROBLEM
        /// This solves the RSA Number J2 Problem from 2005
        /// cemc.math.uwaterloo.ca/contests/computing/2005/stage1/juniorEn.pdf
        /// </summary>
        /// <param name="lowerLimit">Lower limit of the range</param>
        /// <param name="upperLimit">Upper limit of the range</param>
        /// <example>api/Assignment/RSANumbers/11/15</example>
        /// <returns>The number of RSA numbers between 11 and 15 is 2</returns>
        [Route("api/Assignment/RSANumbers/{lowerLimit}/{upperLimit}")]
        [HttpGet]
        public string RSANumbers(int lowerLimit, int upperLimit)
        {
            string output = "The number of RSA numbers between ";
            int countOfDivisors = 0;
            int countOfRSANumbers = 0;
            for (int i = lowerLimit; i <= upperLimit; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    if ((i % j) == 0)
                        countOfDivisors++;
                }

                if (countOfDivisors == 4)
                    countOfRSANumbers++;

                countOfDivisors = 0;
            }
            output = output + lowerLimit.ToString() + " and " + upperLimit.ToString() + " is " + countOfRSANumbers;
            return output;
        }

        /// <summary>
        /// J3 PROBLEM
        /// This problem solves the Problem J3: Are we there yet?
        ///cemc.math.uwaterloo.ca/contests/computing/2018/stage%201/juniorEF.pdf
        /// </summary>
        /// <param name="city1">distance between cities 0 and 1</param>
        /// <param name="city2">distance between cities 1 and 2</param>
        /// <param name="city3">distance between cities 2 and 3</param>
        /// <param name="city4">distance between cities 3 and 4</param>
        /// <example>api/Assignment/CityDistance/3/10/12/5</example>
        /// <returns>
        /// 0 3 13 25 30
        /// 3 0 10 22 27
        /// 13 10 0 12 17
        /// 25 22 12 0 5
        /// 30 27 17 5 0
        /// </returns>
        [Route("api/Assignment/CityDistance/{city1}/{city2}/{city3}/{city4}")]
        [HttpGet]
        public IEnumerable<string> CityDistance(int city1,int city2,int city3,int city4)
        {
            int[] distances = { city1, city2, city3, city4 };
            string[] output = new string[5];
            int i = 0;
            int j = 0;
            int sum = 0;
            string delimiter = " ";
            for (i = 0; i < 4; i++)
            {    if (i - 1 < 0)
                    output[j] = "0" + delimiter;
                
                    for (int k = i-1; k >= 0; k--)
                    {
                        
                        sum += distances[k];
                        output[j] = sum.ToString() + delimiter + output[j];
                    }
                sum = 0;
                if(i!= 0)
                output[j] += "0" + delimiter;
                for (int k = i; k < 4; k++)
                {
                    sum += distances[k];
                    output[j] += sum.ToString() + delimiter;
                }
                sum = 0;
                j++;
            }
            for (int k = 3; k >= 0; k--)
            {
                sum += distances[k];
                output[j] = sum.ToString() + delimiter + output[j];
            }
            output[j] += "0";
            return output;
        }

    }
}
