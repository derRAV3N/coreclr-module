import playerPosition from "./player-position.js"
import proto from "./proto.js"
import streamingWorker from "./streaming-worker.mjs";

export default class EntityRepository {
    constructor(websocket) {
        this.websocket = websocket;
        // entity-id, entity
        this.entities = new Map();
        this.streamedInEntities = new Set();
        const workerBlob = new Blob([streamingWorker], {type: 'application/javascript'});
        this.streamingWorker = new Worker(window.URL.createObjectURL(workerBlob));
        playerPosition.update = (position) => {
            this.streamingWorker.postMessage({
                position: position
            });
        };
        this.streamingWorker.onmessage = event => {
            const streamIn = event.data.streamIn;
            const streamOut = event.data.streamOut;
            if (streamIn !== undefined) {
                this.streamedInEntities.add(streamIn);
                proto.getProto().then((proto) => {
                    websocket.sendEvent({streamIn: proto.EntityStreamInEvent.create({entityId: streamIn})})
                });
                if (!this.entities.has(streamIn)) return;
                let streamInEntity = this.entities.get(streamIn);
                alt.emit("networkingEntityStreamIn", streamInEntity);
            } else if (streamOut !== undefined) {
                this.streamedInEntities.delete(streamOut);
                proto.getProto().then((proto) => {
                    websocket.sendEvent({streamOut: proto.EntityStreamOutEvent.create({entityId: streamOut})})
                });
                if (!this.entities.has(streamOut)) return;
                let streamOutEntity = this.entities.get(streamOut);
                alt.emit("networkingEntityStreamOut", streamOutEntity);
            }
        };
    }

    isStreamedIn(id) {
        return this.streamedInEntities.has(id);
    }

    getEntities() {
        return this.entities.values();
    }

    setEntities(entities) {
        let newEntities = new Map();
        for (const entity of entities) {
            newEntities.set(entity.id, entity);
        }
        this.entities = newEntities;
        this.updateWorker();
    }

    addEntity(entity) {
        this.entities.set(entity.id, entity);
        this.streamingWorker.postMessage({
            position: playerPosition.getPosition(),
            entityToAdd: EntityRepository.copyEntityWithoutData(entity)
        });
    }

    static copyEntityWithoutData(entity) {
        return {
            id: entity.id,
            range: entity.range,
            dimension: entity.dimension,
            position: {
                x: EntityRepository.roundDecimal(entity.position.x, 3),
                y: EntityRepository.roundDecimal(entity.position.y, 3),
                z: EntityRepository.roundDecimal(entity.position.z, 3)
            }
        };
    }

    static roundDecimal(number, precision) {
        let factor = Math.pow(10, precision);
        return Math.round(number * factor) / factor;
    }

    copyEntitiesWithoutData() {
        let copiedEntities = [];
        for (const [_, entity] of this.entities) {
            copiedEntities.push(EntityRepository.copyEntityWithoutData(entity));
        }
        return copiedEntities;
    }

    removeEntity(id) {
        if (!this.entities.has(id)) {
            return;
        }
        let entity = this.entities.get(id);
        if (!entity) return;
        this.entities.delete(id);
        this.streamingWorker.postMessage({
            position: playerPosition.getPosition(),
            entityToRemove: EntityRepository.copyEntityWithoutData(entity)
        });
    }

    updateWorker() {
        this.streamingWorker.postMessage({
            position: playerPosition.getPosition(),
            entities: this.copyEntitiesWithoutData()
        })
    }
}