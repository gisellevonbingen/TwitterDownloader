﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace SNSDownloader.Twitter
{
    public class VideoInfo
    {
        public int AspectRatioW { get; set; } = 0;
        public int AspectRatioH { get; set; } = 0;
        public long Duration { get; set; } = 0L;
        public List<VideoVariant> Variants { get; } = new List<VideoVariant>();

        public VideoInfo()
        {

        }

        public VideoInfo(JToken json)
        {
            var aspectRatio = json.Value<JArray>("aspect_ratio").Select(i => i.Value<int>()).ToArray();
            this.AspectRatioW = aspectRatio[0];
            this.AspectRatioH = aspectRatio[1];
            this.Duration = json.Value<long>("duration_millis");
            this.Variants.AddRange(json.Value<JArray>("variants").Select(t => new VideoVariant(t)));
        }

        public JObject ToJson() => new JObject()
        {
            ["aspect_ratio"] = new JArray(new int[] { this.AspectRatioW, this.AspectRatioH }),
            ["duration_millis"] = this.Duration,
            ["variants"] = new JArray(this.Variants.Select(t => t.ToJson()).ToArray()),
        };

    }

}
