/*!
The Apache License 2.0 License

Copyright (c) 2017 Dimitris Katikaridis <dkatikaridis@gmail.com>,Giannis Menekses <johnmenex@hotmail.com>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
namespace kagv {

    public partial class MainForm {

        //function that starts the needed timers
        private void Timers() {
            //every timer is responsible for every agv for up to 5 _AGVs

            int c = 0;
            for (int i = 0; i < _trappedStatus.Length; i++)
                if (!_trappedStatus[i]) //array containing the status of AGV
                    c++; //counts the number of free-to-move _AGVs

            switch (c) //depending on the _c, the required timers will be started
            {
                case 1:
                    timer0.Start();
                    break;
                case 2:
                    timer0.Start();
                    timer1.Start();
                    break;
                case 3:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    break;
                case 4:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    break;
                case 5:
                    timer0.Start();
                    timer1.Start();
                    timer2.Start();
                    timer3.Start();
                    timer4.Start();
                    break;
            }
        }
    }
}
