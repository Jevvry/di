﻿using Microsoft.Extensions.DependencyInjection;
using TagsCloud.Factory;
using TagsCloud.ImageProcessing.SaverImage.ImageSavers;
using TagsCloud.Layouter;
using TagsCloud.TagsCloudProcessing.TagsGenerators;
using TagsCloud.TextProcessing.Converters;
using TagsCloud.TextProcessing.TextFilters;
using TagsCloud.TextProcessing.TextReaders;

namespace TagsCloudUI.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static ServiceProvider ConfigureFactories(this ServiceProvider serviceProvider)
        {
            serviceProvider.GetService<IServiceFactory<IImageSaver>>()
                .Register("Сохранить в png", serviceProvider.GetService<PngSaver>)
                .Register("Сохранить в jpg", serviceProvider.GetService<JpgSaver>)
                .Register("Сохранить в bmp", serviceProvider.GetService<BmpSaver>);

            serviceProvider.GetService<IServiceFactory<IWordsReader>>()
                .Register("Файл txt", serviceProvider.GetService<TxtReader>)
                .Register("Файл doc", serviceProvider.GetService<DocReader>);

            serviceProvider.GetService<IServiceFactory<IRectanglesLayouter>>()
                .Register("По спирали", serviceProvider.GetRequiredService<CircularCloudLayouterCreator>().Create);

            serviceProvider.GetService<IServiceFactory<ITagsGenerator>>()
                .Register("Топ 30", serviceProvider.GetService<TagsGenerator>);

            serviceProvider.GetService<IServiceFactory<IWordConverter>>()
                .Register("Перевести в нижний регистр", serviceProvider.GetService<LowerCaseConverter>);

            serviceProvider.GetService<IServiceFactory<ITextFilter>>()
                .Register("Исключить служебные символы", serviceProvider.GetService<FunctionWordsFilter>);

            return serviceProvider;
        }
    }
}
