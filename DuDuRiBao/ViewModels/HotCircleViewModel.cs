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

using Brook.DuDuRiBao.Common;
using Brook.DuDuRiBao.Models;
using Brook.DuDuRiBao.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brook.DuDuRiBao.ViewModels
{
    public partial class HotCircleViewModel : ViewModelBase
    {
        private string _hotCircle;
        public string HotCircle
        {
            get { return _hotCircle; }
            set
            {
                if (value != _hotCircle)
                {
                    _hotCircle = value;
                    Notify("HotCircle");
                }
            }
        }

        public string Title
        {
            get { return StringUtil.GetString("HotCircle"); }
        }

        public override void Init()
        {
            RefreshHotCircle();
        }

        public async void RefreshHotCircle()
        {
            HotCircle = await DataRequester.RequestHotCircles();
        }
    }
}
