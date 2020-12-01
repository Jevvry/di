﻿using System.Collections.Generic;
using System.Linq;
using TagsCloud.TextProcessing.WordConfig;

namespace TagsCloud.TextProcessing.Converters
{
    public class ConvertersApplier : IConvertersApplier
    {
        private readonly Dictionary<string, IWordConverter> wordConverters;
        private readonly IWordsConfig wordsConfig;

        public ConvertersApplier(IWordsConfig wordsConfig)
        {
            wordConverters = new Dictionary<string, IWordConverter>();
            this.wordsConfig = wordsConfig;
        }

        public IEnumerable<string> ApplyConversion(IEnumerable<string> words)
        {
            var converterNames = wordsConfig.ConvertersNames;
            return words.Select(word => converterNames
                                        .Select(converterName => wordConverters[converterName])
                                        .Aggregate(word, (current, converter) => converter.Convert(current)));
        }

        public IEnumerable<string> GetConverterNames() => wordConverters.Select(pair => pair.Key);

        public void Register(string converterName, IWordConverter wordConverter) =>
                                                                    wordConverters[converterName] = wordConverter;
    }
}
