using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tempestas_mons.domain;
using tempestas_mons.domain.models;
using tempestas_mons.domain.services;
using tempestas_mons.web.Models.roadconditions;

namespace tempestas_mons.web.Controllers
{
    public class RoadConditionController : Controller
    {
        private readonly RoadConditionRepository _roadConditionRepository;

        public RoadConditionController():this(new RoadConditionRepository(new StreamReaderFactory()))
        {
            
        }

        public RoadConditionController(RoadConditionRepository roadConditionRepository)
        {
            _roadConditionRepository = roadConditionRepository;
        }

        public ViewResult Index()
        {
            var viewModel = new RoadConditionIndexViewModel
            {
                StartDate = DateTime.Today.ToString(),
                EndDate = DateTime.Today.AddDays(-1).ToString(),
                Summary = new RoadConditionSummaryViewModel
                {
                    PercentChainsRequiredAllVehicles = 0,
                    PercentChainsRequiredExceptAllWheelDrive = 0,
                    PercentPassClosed = 0,
                    PercentTractionTiresAdvised = 0,
                    PercentTractionTiresRequired = 0,
                    PercentTrafficDelayed = 0,
                    PercentTrafficDelayedForAvalancheControl = 0,
                    PercentTrafficStoppedForAvalancheControl = 0,
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        public ViewResult Index(string startDate, string endDate, string direction)
        {
            var start = DateTime.Parse(startDate);
            var end = DateTime.Parse(endDate);

            Direction? trafficDirection = null;
            if (direction != "All")
                trafficDirection = (Direction)Enum.Parse(typeof(Direction), direction);

            var dataInRange = _roadConditionRepository.Get()
                .Where(d => d.Start >= start)
                .Where(d => d.End <= end)
                .SelectMany(d => d.TravelRestrictions);

            if(trafficDirection.HasValue)
                dataInRange = dataInRange.Where(d => d.Direction == trafficDirection);

            var summary = MapSummary(dataInRange);

         
            var viewModel = new RoadConditionIndexViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Direction = direction,
                Summary = summary
            };


            return View(viewModel);
        }

        private RoadConditionSummaryViewModel MapSummary(IEnumerable<TravelRestriction> restrictionsInRange)
        {
            var realizedRestrictions = restrictionsInRange.ToList();

            var count = realizedRestrictions.Count;

            var chainsRequiredAllVehiclesCount = realizedRestrictions.Count(r=>r.Restrictions.Contains(Restriction.ChainsRequiredAllVehicles));
            var chainsRequiredExceptAWDCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.ChainsRequiredExceptAllWheelDrive));
            var passClosedCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.PassClosed));
            var tractionTiresAdvisedCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.TractionTiresAdvised));
            var tractionTiresRequiredCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.TractionTiresRequired));
            var trafficDelayedCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.TrafficDelayed));
            var trafficDealyedForAvalancheControlCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.TrafficDelayedForAvalancheControl));
            var trafficStoppedForAvalancheControlCount = realizedRestrictions.Count(r => r.Restrictions.Contains(Restriction.TrafficStoppedForAvalancheControl));

            var roadConditionSummaryViewModel = new RoadConditionSummaryViewModel
            {
                PercentChainsRequiredAllVehicles = Percentage(chainsRequiredAllVehiclesCount,count),
                PercentChainsRequiredExceptAllWheelDrive = Percentage(chainsRequiredExceptAWDCount,count),
                PercentPassClosed = Percentage(passClosedCount,count),
                PercentTractionTiresAdvised = Percentage(tractionTiresAdvisedCount,count),
                PercentTractionTiresRequired = Percentage(tractionTiresRequiredCount,count),
                PercentTrafficDelayed = Percentage(trafficDelayedCount,count),
                PercentTrafficDelayedForAvalancheControl = Percentage(trafficDealyedForAvalancheControlCount,count),
                PercentTrafficStoppedForAvalancheControl = Percentage(trafficStoppedForAvalancheControlCount,count),
                
            };

            return roadConditionSummaryViewModel;
        }

        public ViewResult RangeAnalysis()
        {
            var viewModel = new RoadConditionRangeAnalysisViewModel
            {
                Month = DateTime.Today.ToString("MMMM"),
                Day = 1,
                Range = 5,
                Summary = new RoadConditionSummaryViewModel
                {
                    PercentChainsRequiredAllVehicles = 0,
                    PercentChainsRequiredExceptAllWheelDrive = 0,
                    PercentPassClosed = 0,
                    PercentTractionTiresAdvised = 0,
                    PercentTractionTiresRequired = 0,
                    PercentTrafficDelayed = 0,
                    PercentTrafficDelayedForAvalancheControl = 0,
                    PercentTrafficStoppedForAvalancheControl = 0,
                }
            };

            return View(viewModel);
        }

        private double Percentage(int sum, int count)
        {
            if (count == 0)
                return 0;

            var result=  (double)sum/(double)count*100;

            return result;
        }
    }
}