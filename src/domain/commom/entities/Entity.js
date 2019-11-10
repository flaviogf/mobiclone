import uuid4 from 'uuid/v4'

class Entity {
  constructor({ id = '' }) {
    this._id = id || uuid4()
  }

  async id() {
    return this._id
  }
}

export default Entity
