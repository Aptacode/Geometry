using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Aptacode.Geometry.Demo.Pages;

public class IndexBase : ComponentBase
{
    public DemoSceneController SceneController { get; set; }

    protected override async Task OnInitializedAsync()
    {
        //Scene
        SceneController = new DemoSceneController(new Vector2(200, 100));
        await base.OnInitializedAsync();
    }
}