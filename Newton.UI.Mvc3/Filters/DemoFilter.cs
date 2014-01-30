//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Newton.Data;

//namespace Newton.UI.Mvc3
//{
//    public class DemoRecord
//    {
//        public DateTime Date { get; }
//    }

//    public class DemoFilter : IFilter<DemoRecord>
//    {
//        private IFilterInterpretor<DemoRecord, DemoFilter> queryInterpretor;

//        public DateTime Start { get; set; }
//        public DateTime End { get; set; }
//        public string Type { get; set; }

//        public IQuery<DemoRecord> AsQuery()
//        {
//            return queryInterpretor.Convert(this);
//        }
//    }

//    public class DemoFilterInterpretor : IFilterInterpretor<DemoRecord, DemoFilter>
//    {
//        public IQuery<DemoRecord> Convert(DemoFilter filter)
//        {
//            return new Newton.Data.PredicateQuery<DemoRecord>()
//            {
//                Predicate = (p) =>
//                    p.Date < filter.End &&
//                    p.Date >= filter.Start
//            };            
//        }
//    }


//}
