using Microsoft.Xna.Framework;
using Nez;
using Nez.Samples;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace platformowka
{
    class MasterScene: Scene
    {
        public override void initialize()
        {
            clearColor = Color.LightGray;
            addRenderer(new DefaultRenderer());
            var tiledMaps= content.Load<TiledMap>("map/tilemap");
            var objectLayer = tiledMaps.getObjectGroup("objects");
           // var spawn = objectLayer.objectWithName("spawn");
            var tiledEntity = createEntity("tiled-map");
            tiledEntity.addComponent(new TiledMapComponent(tiledMaps));
            var tiledMapComponent = tiledEntity.addComponent(new TiledMapComponent(tiledMaps, "collision"));
            tiledMapComponent.setLayersToRender(new string[] { "tiles", "terrain", "details" });
            // render below/behind everything else. our player is at 0 and projectile is at 1.
            tiledMapComponent.renderLayer = 10;
            // render our above-details layer after the player so the player is occluded by it when walking behind things
            var tiledMapDetailsComp = tiledEntity.addComponent(new TiledMapComponent(tiledMaps));
            tiledMapDetailsComp.setLayerToRender("above-details");
            tiledMapDetailsComp.renderLayer = -1;
            // the details layer will write to the stencil buffer so we can draw a shadow when the player is behind it. we need an AlphaTestEffect
            // here as well
            tiledMapDetailsComp.material = Material.stencilWrite();
           // tiledMapDetailsComp.material.effect = content.loadNezEffect<SpriteAlphaTestEffect>();

            // setup our camera bounds with a 1 tile border around the edges (for the outside collision tiles)
           // tiledEntity.addComponent(new CameraBounds(new Vector2(tiledMaps.tileWidth, tiledMaps.tileWidth), new Vector2(tiledMaps.tileWidth * (tiledMaps.width - 1), tiledMaps.tileWidth * (tiledMaps.height - 1))));


            var playerEntity = createEntity("player", new Vector2(256 / 2, 224 / 2));
            playerEntity.addComponent(new Ninja());
            var collider = playerEntity.addComponent<CircleCollider>();
            // we only want to collide with the tilemap, which is on the default layer 0
            Flags.setFlagExclusive(ref collider.collidesWithLayers, 0);
            // move ourself to layer 1 so that we dont get hit by the projectiles that we fire
            Flags.setFlagExclusive(ref collider.physicsLayer, 1);

            // add a component to have the Camera follow the player
           // camera.entity.addComponent(new FollowCamera(playerEntity));

            // stick something to shoot in the level
          
            //var player = createEntity("player");
            //player.transform.setPosition(spawn.x, spawn.y);
            //PrototypeSprite prototypeSprite = new PrototypeSprite(16, 32);
            //player.addComponent(component :prototypeSprite.setColor(Color.Red));
           


        }
    }
}
