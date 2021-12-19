﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;

using System.Collections.Generic;
using System.Security.Cryptography;

namespace GeoJSON.Text.Test.Benchmark
{
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net50)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [RPlotExporter]
    public class BenchmarkSerializeFeatureLinestring
    {
        // GeoJson.NET
        private Net.Feature.Feature<Net.Geometry.LineString, Dictionary<string, string>>? featureLinestringGeoJsonNET;

        // GeoJson.Text
        private Text.Feature.Feature<Text.Geometry.LineString, Dictionary<string, string>>? featureLinestringGeoJsonTEXT;

        [GlobalSetup]
        public void Setup()
        {
            var lineCoordinates = new List<List<double>>
                {
                    new List<double>
                    {
                        -0.26092529296875,
                        51.470691106434884
                    },
                    new List<double>
                    {
                        -1.26092529296875,
                        51.470691106434884
                    },
                    new List<double>
                    {
                        -2.26092529296875,
                        51.470691106434884
                    },
                    new List<double>
                    {
                        -3.26092529296875,
                        51.470691106434884
                    },
                    new List<double>
                    {
                        -4.26092529296875,
                        51.470691106434884
                    },
                    new List<double>
                    {
                        -5.26092529296875,
                        51.470691106434884
                    },
                };

            var linestringNET = new Net.Geometry.LineString(lineCoordinates);
            var linestringTEXT = new Text.Geometry.LineString(lineCoordinates);

            Dictionary<string, string> props = new Dictionary<string, string>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };

            featureLinestringGeoJsonNET = new Net.Feature.Feature<Net.Geometry.LineString, Dictionary<string, string>>(linestringNET, props, "1");
            featureLinestringGeoJsonTEXT = new Text.Feature.Feature<Text.Geometry.LineString, Dictionary<string, string>>(linestringTEXT, props, "1");

        }

        [Benchmark]
        public void SerializeNewtonsoft()
        {
            Newtonsoft.Json.JsonConvert.SerializeObject(featureLinestringGeoJsonNET);
        }

        [Benchmark]
        public void SerializeSystemTextJson()
        {
            System.Text.Json.JsonSerializer.Serialize(featureLinestringGeoJsonTEXT);
        }
    }
}
