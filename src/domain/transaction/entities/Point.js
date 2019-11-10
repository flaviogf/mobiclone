import Entity from '../../commom/entities/Entity'

class Point extends Entity {
  constructor({ description, value = 0, id = '' }) {
    super({ id })

    this._description = description
    this._value = value
  }

  async description() {
    return this._description
  }

  async value() {
    return this._value
  }
}

export default Point
