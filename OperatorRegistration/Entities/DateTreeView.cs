using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatorRegistration.Utilities;

namespace OperatorRegistration.Entities
{
    /// <summary>
    /// Scripter : YONGTOK KIM
    /// Scripted Date : 1 Feb 2012  
    /// Set the Data for the TreeView by Date
    /// </summary>
    /// 

    #region Property

    public class TrainingDate
    {
        public string DateOfTraining { get; set; }
        public string NoOfOperators { get; set; }

        public TrainingDate() { }
        public TrainingDate(string dateOfTraining, string noOfOperators)
        {
            DateOfTraining = dateOfTraining;
            NoOfOperators = noOfOperators;
        }
    }

    #endregion Property


    class DateTreeView
    {
        public List<TrainingDate> GetDates(PersonAndDbidsOperators operators)
        {
            //PersonAndDbidsOperators operators = new PersonAndDbidsOperators();
            //DeserializeXml dx = new DeserializeXml();
            //operators = dx.DeserializePersons();

            List<TrainingDate> listToReturn = new List<TrainingDate>();
            int countByDate = 0;


            var dates = operators.Persons.Select(p => p.LastTrainedDate).Distinct();
            int totalNumbers = operators.Persons.Count();

            if (dates != null)
            {
                foreach (var date in dates)
                {
                    if (date != null)
                    {
                        countByDate = operators.Persons.Where(p => p.LastTrainedDate.Equals(date)).Count();
                        listToReturn.Add(new TrainingDate(date, string.Format(" ({0})", countByDate.ToString())));
                    }
                }
            }

            return listToReturn;
        }
        
    }
}
