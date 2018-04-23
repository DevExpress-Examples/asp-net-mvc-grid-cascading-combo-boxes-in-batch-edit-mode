using DevExpress.Web;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace E4425.Models
{
    public class ComboBoxPropertiesProvider : IDisposable
    {

        static ComboBoxPropertiesProvider current;
        public static ComboBoxPropertiesProvider Current
        {
            get
            {
                if (current == null)
                    current = new ComboBoxPropertiesProvider();
                return current;
            }
        }

        public int EditableCountryID
        {
            get
            {
                string rawCountryId = HttpContext.Current.Request["CountryId"];
                return string.IsNullOrEmpty(rawCountryId) ? -1 : int.Parse(rawCountryId);
            }
        }
        MVCxColumnComboBoxProperties cityComboBoxProperties;
        public MVCxColumnComboBoxProperties CityComboBoxProperties
        {
            get
            {
                if (cityComboBoxProperties == null)
                    cityComboBoxProperties = CreateCityComboBoxProperties();
                return cityComboBoxProperties;
            }
        }

        MVCxColumnComboBoxProperties countryComboBoxProperties;
        public MVCxColumnComboBoxProperties CountryComboBoxProperties
        {
            get
            {
                if (countryComboBoxProperties == null)
                    countryComboBoxProperties = CreateCountryComboBox();
                return countryComboBoxProperties;
            }
        }

        WorldCitiesEntities worldCities;
        protected WorldCitiesEntities WorldCities
        {
            get
            {
                if (worldCities == null)
                    worldCities = new WorldCitiesEntities();
                return worldCities;
            }
        }

        protected MVCxColumnComboBoxProperties CreateCountryComboBox()
        {
            MVCxColumnComboBoxProperties cs = new MVCxColumnComboBoxProperties();
            cs.CallbackRouteValues = new { Controller = "Home", Action = "ComboBoxCountryPartial" };
            cs.Width = Unit.Percentage(100);
            cs.TextField = "CountryName";
            cs.ValueField = "CountryId";
            cs.ValueType = typeof(int);
            cs.IncrementalFilteringDelay = 1000;
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cs.FilterMinLength = 2;
            cs.CallbackPageSize = 20;
            cs.ClientSideEvents.SelectedIndexChanged = "CountriesCombo_SelectedIndexChanged";
            cs.BindList(WorldCities.Countries.ToList());
            return cs;
        }
        protected MVCxColumnComboBoxProperties CreateCityComboBoxProperties()
        {
            MVCxColumnComboBoxProperties cs = new MVCxColumnComboBoxProperties();
            cs.CallbackRouteValues = new { Controller = "Home", Action = "ComboBoxCityPartial" };
            cs.Width = Unit.Percentage(100);
            cs.CallbackPageSize = 20;
            cs.TextField = "CityName";
            cs.ValueField = "CityId";
            cs.ValueType = typeof(int);
            cs.IncrementalFilteringDelay = 1000;
            cs.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cs.FilterMinLength = 2;
            cs.ValidationSettings.Display = Display.None;
            cs.BindList(OnItemsRequestedByFilterCondition, OnItemRequestedByValue);
            cs.ClientSideEvents.BeginCallback = "CitiesCombo_BeginCallback";
            cs.ClientSideEvents.EndCallback = "CitiesCombo_EndCallback";
            return cs;
        }

        protected object OnItemRequestedByValue(ListEditItemRequestedByValueEventArgs e)
        {
            int id;
            if (e.Value == null || !int.TryParse(e.Value.ToString(), out id))
                return null;
            var query = WorldCities.Cities.Where(city => city.CityId == id);
            return query.ToList();

        }
        protected object OnItemsRequestedByFilterCondition(ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            IQueryable<City> query;
            var skip = e.BeginIndex;
            var take = e.EndIndex - e.BeginIndex + 1;
            if (EditableCountryID > -1)
                query = WorldCities.Cities.Where(city => city.CityName.Contains(e.Filter) && city.Country.CountryId == EditableCountryID).OrderBy(city => city.CityId);
            else
                query = WorldCities.Cities.Where(city => city.CityName.Contains(e.Filter)).OrderBy(city => city.CityId);
            query = query.Skip(skip).Take(take);
            return query.ToList();
        }

        #region IDisposable Members
        public void Dispose()
        {
            if (this.worldCities != null)
                worldCities.Dispose();
        }
        #endregion
    }
}