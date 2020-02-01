import 'package:equatable/equatable.dart';

abstract class SignInWithEmailEvent extends Equatable {}

class SubmitSignInWithEmailEvent extends SignInWithEmailEvent {
  final String email;
  final String password;

  SubmitSignInWithEmailEvent(this.email, this.password);

  @override
  List<Object> get props => [];
}
