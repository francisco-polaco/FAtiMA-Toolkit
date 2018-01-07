﻿using System;

namespace MCTS.DST.Objects {
        public class PickableObject {

            public Boolean Pickable { get; set; } = false;

            private Boolean PosXSet { get; set; } = false;
            private Boolean PosZSet { get;  set; } = false;
            private Boolean EntitySet {  get;  set; } = false;
                                       
            public string Guid { get; }

            private int posX;

            public int GetPosX() {
                return posX;
            }

            public void SetPosX(int value) {
                PosXSet = true;
                posX = value;
            }

            private int posZ;

            public int GetPosZ() {
                return posZ;
            }

            public void SetPosZ(int value) {
                PosZSet = true;
                posZ = value;
            }

            private string entityType;

            public string GetEntityType() {
                return entityType;
            }

            public void SetEntityType(string value) {
                EntitySet = true;
                entityType = value;
            }

            public PickableObject(string guid) {
                Guid = guid;
            }

            public bool isPickableComplete() {
                return Pickable && EntitySet && PosXSet && PosZSet;
            }

        }

    }
}
