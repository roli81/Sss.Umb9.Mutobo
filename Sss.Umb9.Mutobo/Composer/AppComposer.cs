﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sss.Umb9.Mutobo.Interfaces;
using Sss.Umb9.Mutobo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;


namespace Sss.Umb9.Mutobo.Composer
{
    public class AppComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            RegisterServices(builder);
        }


        private void AddComponents(IUmbracoBuilder builder)
        {
            //composition.Components().Append<ApiConfigurationComponent>();
            //composition.Components().Append<SearchConfigurationComponent>();
            //composition.Components().Append<HtmlMinifierComponent>();
            //composition.Components().Append<CustomDropDownPopulateComponent>();
            //composition.Components().Append<CustomIndexComponent>();
        }

        private void RegisterServices(IUmbracoBuilder builder)
        {
           
            builder.Services.AddSingleton<IImageService, ImageService>();
            builder.Services.AddSingleton<IConfigurationService, ConfigurationService>();
            builder.Services.AddSingleton<IMutoboContentService, MutoboContentService>();
        }
    }
}