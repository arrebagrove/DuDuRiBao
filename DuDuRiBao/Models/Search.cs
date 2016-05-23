﻿#region License
//   Copyright 2015 Brook Shi
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License. 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brook.DuDuRiBao.Models
{
    public class SearchCirclCount
    {
        public int Stories { get; set; }
        public int Members { get; set; }
    }
    public class SearchCircle : CircleBase
    {
        public SearchCirclCount Count { get; set; }
        public string Description { get; set; }
    }
    public class SearchCircles
    {
        public IEnumerable<SearchCircle> Circles { get; set; }
    }

    public class SearchStory
    {
        public string External_Url { get; set; }
        public string Summary { get; set; }
        public int Type { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class SearchStories
    {
        public IEnumerable<SearchStory> Stories { get; set; }
    }

    public class PeopleCount
    {
        public int Posts { get; set; }
        public int Followers { get; set; }
        public int Likes { get; set; }
    }
    public class SearchUser
    {
        public PeopleCount Count { get; set; }
        public string Bio { get; set; }
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
    public class SearchUsers
    {
        public IEnumerable<SearchUser> Users { get; set; }
    }
}
