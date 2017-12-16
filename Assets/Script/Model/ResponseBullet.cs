using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class ResponseBullet
    {
    private string message;
    private string randomNote;
    //private int objId;

    public ResponseBullet() {

    }
    public ResponseBullet(string message, string randomNote)//, int objId)
    {
        this.message = message;
        this.randomNote = randomNote;
        //this.objId = objId;
    }

    //public int ObjId { get; set; }
    public string RandomNote { get; set; }
    public string Message { get; set; }

}

