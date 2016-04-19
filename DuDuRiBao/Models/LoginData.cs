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


namespace Brook.DuDuRiBao.Models
{
    public class LoginData
    {
        public string refresh_token { get; set; }
        public string source { get; set; }
        public int expires_in { get; set; }
        public string access_token { get; set; }
        public string user { get; set; }
    }
}
