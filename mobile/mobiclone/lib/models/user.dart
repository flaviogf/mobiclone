import 'package:equatable/equatable.dart';

class User extends Equatable {
  final String name;
  final String email;
  final String password;

  User(this.name, this.email, this.password);

  @override
  List<Object> get props => [name, email, password];
}
