window.updateUI = function () {

    var result = document.getElementById("hdResult").value;
    console.log(result);
    document.getElementById("hdResult").value = "";

    var equation = document.getElementById("txtEquation").value;

    var arrResult = result.split(';');
        
    var plotIt = function () {
       

        document.getElementById("spResult").innerHTML = "Root : " + arrResult[0];
        
        var point = [arrResult[0], 0];
        var options = {
            title: equation,
            target: "#plot",
            width: 800,
            height: 600,
            //disableZoom: true,
            xAxis: {
                label: "x - axis",
                domain: [-10, 10]
            },
            yAxis: {
                label: "y - axis"
            },
            data: [
                { fn: equation },
                {
                    points: [point],
                    fnType: 'points',
                    graphType: 'scatter',
                    color: 'red'
                }
            ]
        };
        functionPlot(options);

        if (arrResult.length > 1) {
            arrResult = arrResult.slice(1);
            setTimeout(function () { plotIt() }, 200);
        }
    }

    if (arrResult && arrResult.length > 0)
    {
        setTimeout(function () { plotIt() }, 200);
    }
    

    


}


window.onload = function()
{
    

    setInterval(function () {
        var result = document.getElementById("hdResult").value;
        if (result && result != "")
        {
            updateUI();
        }
    }, 1000);
}