using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            // var async = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Single);
            // async.allowSceneActivation = true;
            // async.completed += operation =>
            // {
            // };
            InitGame();
        }


        private void InitGame()
        {
            List<MonoBehaviour> gameObjects =
                FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                    .Distinct().ToList();

            List<IHaveInit> initables = new List<IHaveInit>();

            foreach (var go in gameObjects)
            {
                var initable = go.GetComponent<IHaveInit>();
                if (initable != null)
                {
                    initables.Add(initable);
                }
            }

            initables = initables.OrderBy(x => x.Order).ToList();
            initables.ForEach(x => x.Init());

            List<IHavePostInit> postInitables = new List<IHavePostInit>();

            foreach (var go in gameObjects)
            {
                var postInitable = go.GetComponent<IHavePostInit>();
                if (postInitable != null)
                {
                    postInitables.Add(postInitable);
                }
            }

            postInitables = postInitables.OrderBy(x => x.PostInitOrder).ToList();
            postInitables.ForEach(x => x.PostInit());
        }
    }
}