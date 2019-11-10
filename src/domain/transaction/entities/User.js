import Entity from '../../commom/entities/Entity'
import Point from './Point'

class User extends Entity {
  constructor({ name, points = [], totalOfPoints = 0, id = '' }) {
    super({ id })

    this._name = name
    this._points = points
    this._totalOfPoints = totalOfPoints
  }

  async name() {
    return this._name
  }

  async points() {
    return this._points
  }

  async totalOfPoints() {
    return this._totalOfPoints
  }

  async addPoint({ description, value }) {
    const point = new Point({ description, value })
    this._points.push(point)
    this._totalOfPoints += value
    return point
  }
}

export default User
