import 'package:equatable/equatable.dart';

abstract class PasswordEvent extends Equatable {}

class SubmitPasswordEvent extends PasswordEvent {
  final String value;

  SubmitPasswordEvent(this.value);

  @override
  List<Object> get props => [];
}
